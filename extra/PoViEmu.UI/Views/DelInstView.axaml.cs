using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.Inventory.Config;
using PoViEmu.UI.Routes;
using PoViEmu.UI.Tools;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Views
{
    public partial class DelInstView : UserControl
    {
        public DelInstView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (this.FindData<DelInstViewModel>() is { } model)
            {
                if (CfgRepo.Instance.Entities is { } ent)
                {
                    var id = model.InstanceId;
                    if (ent.TryGetValue(id, out var o) && o != null)
                    {
                        model.InstanceName = o.Name;
                    }
                }
            }
        }

        private void OnNo(object? sender, RoutedEventArgs e)
        {
            this.GetRouter().GoBack();
        }

        private void OnYes(object? sender, RoutedEventArgs e)
        {
            if (this.FindData<DelInstViewModel>() is not { } model)
                return;

            if (CfgRepo.Instance.Entities is { } ent)
            {
                var id = model.InstanceId;
                ent.Remove(id);
            }
            this.GetRouter().Push<InstanceViewModel>();
        }
    }
}