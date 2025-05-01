using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.Hyper;
using PoViEmu.UI.Dbg.Core;
using PoViEmu.UI.Tools;

// ReSharper disable AsyncVoidMethod
// ReSharper disable UnusedType.Global

namespace PoViEmu.UI.Dbg.Views
{
    public partial class RunDbgView : UserControl
    {
        public RunDbgView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
            StartBtn.IsEnabled = false;
            StopBtn.IsEnabled = true;
        }

        private RunUtil.RunObj? _currentInfo;
        private IVMachine? _currentMach;

        private async Task<RunUtil.RunObj?> GetCurrentRunObj(DbgUiTool.DbgRun rm)
        {
            if (_currentInfo != null)
                return _currentInfo;
            var run = rm.Run;
            var entity = await RunUtil.FindEntity(run.InstanceId);
            _currentInfo = entity;
            return entity;
        }

        private async Task<IVMachine?> GetCurrentMachine(DbgUiTool.DbgRun rm)
        {
            if (_currentMach != null)
                return _currentMach;
            if (await GetCurrentRunObj(rm) is not { } runObj)
                return null;
            var hyper = Hypervisor.Default;
            var machine = hyper.GetRunning(runObj.Entity.Id);
            _currentMach = machine;
            return machine;
        }

        private async void StopBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            if (this.GetCurrentRun() is not { } ro) return;
            await Task.Run(async () =>
            {
                if (await GetCurrentMachine(ro) is not { } machine) return;
                machine.Stop();
            });
            StopBtn.IsEnabled = false;
            StartBtn.IsEnabled = true;
        }

        private async void StartBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            if (this.GetCurrentRun() is not { } ro) return;
            await Task.Run(async () =>
            {
                if (await GetCurrentMachine(ro) is not { } machine) return;
                machine.Start();
            });
            StartBtn.IsEnabled = false;
            StopBtn.IsEnabled = true;
        }
    }
}