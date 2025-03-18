using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.Inventory.Config;
using PoViEmu.UI.Routes;
using PoViEmu.UI.Tools;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Views
{
    public partial class EditInstView : UserControl
    {
        public EditInstView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (this.FindData<EditInstViewModel>() is { } model)
            {
                if (CfgRepo.Instance.Entities is { } ent)
                {
                    var id = model.InstanceId;
                    var o = ent[id];
                    model.InstanceName = o.Name;
                    model.InstanceNotes = o.Notes;
                    model.TemplateName = o.Template;
                }
                model.ForceCheck();
            }
        }

        private void OnNextClick(object? sender, RoutedEventArgs e)
        {
            if (this.FindData<EditInstViewModel>() is not { } model)
                return;

            if (CfgRepo.Instance.Entities is { } ent)
            {
                var id = model.InstanceId;
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