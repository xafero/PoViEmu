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
            var ctx = this.GetOrCreateData<InstanceViewModel>();
            CtxUtil.Invoke(async () =>
                {
                    var repo = CfgRepo.Instance;
                    await repo.Load();
                    return repo.Entities.Values.ToArray();
                },
                entries => { ctx.Instances = entries; },
                _ =>
                {
                    /* "Could not load instances!" */
                });
        }

        private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
        }
    }
}