using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.Inventory.Config;
using PoViEmu.UI.Tools;
using PoViEmu.UI.ViewModels;

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
            if (CfgRepo.Instance.Entities is { } ent &&
                this.FindData<InstanceViewModel>() is { } model)
            {
                model.Instances = ent.Values.ToList();
            }
        }

        private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
        }
    }
}