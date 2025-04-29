using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.Hyper;
using PoViEmu.UI.Routes;
using PoViEmu.UI.Tools;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Views
{
    public partial class RunInstView : UserControl
    {
        public RunInstView()
        {
            InitializeComponent();
        }

        private void DoExit(object? sender, RoutedEventArgs e)
        {
            this.GetRouter().GoBack();
        }

        private void DoChangeView(object? sender, RoutedEventArgs e)
        {
            if (this.FindData<RunInstViewModel>() is { } vm)
            {
                var oldState = vm.ViewIsMinimal;
                vm.ViewIsMinimal = !oldState;
            }
        }

        private IVMachine? _vm;

        private void HasLoaded(object? sender, RoutedEventArgs e)
        {
            if (this.FindData<RunInstViewModel>() is { } m)
                Task.Run(async () =>
                {
                    if ((await RunUtil.FindEntity(m.InstanceId)) is not { } runObj)
                        return;
                    var cpuKind = runObj.ProcessorKind;
                    var cfg = new VMConfig(cpuKind, 10, 1);
                    var hypervisor = Hypervisor.Default;
                    var vm = _vm = hypervisor.Create(cfg);
                    vm.Start();
                });
        }

        private void HasUnloaded(object? sender, RoutedEventArgs e)
        {
            if (this.FindData<RunInstViewModel>() is { } m)
            {
                _vm?.Stop();
            }
        }
    }
}