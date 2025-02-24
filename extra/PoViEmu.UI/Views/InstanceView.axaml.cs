using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.Inventory.Config;
using PoViEmu.UI.Tools;

namespace PoViEmu.UI.Views
{
    public partial class InstanceView : UserControl
    {
        public InstanceView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
        }

        private void OnNextClick(object? sender, RoutedEventArgs e)
        {
            if (CfgRepo.Instance.Entities is { } ent)
            {
                var id = Guid.NewGuid();
                ent[id] = new OneEntity { Id = id };
            }
            this.GetRouter().Push<IRoutable>(default);
        }
    }
}