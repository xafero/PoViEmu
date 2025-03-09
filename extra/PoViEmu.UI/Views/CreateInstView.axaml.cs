using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.Inventory.Config;
using PoViEmu.UI.Routes;
using PoViEmu.UI.Tools;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Views
{
    public partial class CreateInstView : UserControl
    {
        public CreateInstView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (this.FindData<CreateInstViewModel>() is { } model)
                model.ForceCheck();
        }

        private void OnNextClick(object? sender, RoutedEventArgs e)
        {
            if (this.FindData<CreateInstViewModel>() is not { } model)
                return;

            if (CfgRepo.Instance.Entities is { } ent)
            {
                var id = Guid.NewGuid();
                ent[id] = new OneEntity
                {
                    Id = id, Name = model.InstanceName,
                    Notes = model.InstanceNotes,
                    Template = model.TemplateName
                };
            }
            this.GetRouter().Push<InstanceViewModel>();
        }

        private void OnBackClick(object? sender, RoutedEventArgs e)
        {
            this.GetRouter().GoBack();
        }
    }
}