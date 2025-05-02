using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.Hyper;
using PoViEmu.UI.Dbg.Core;
using PoViEmu.UI.Dbg.Models;
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

        private async void OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (this.GetCurrentRun() is not { } rvm)
                return;
            var dbg = rvm.Dbg;
            await Task.Run(async () =>
            {
                dbg.CurrentInfo = await GetCurrentRunObj(rvm);
                dbg.CurrentMach = await GetCurrentMachine(rvm);
            });
            StartBtn.IsEnabled = false;
            StopBtn.IsEnabled = true;
            dbg.SendInitialized(new InitEventArgs());
        }

        private static async Task<RunUtil.RunObj?> GetCurrentRunObj(DbgUiTool.DbgRun rm)
        {
            var run = rm.Run;
            var entity = await RunUtil.FindEntity(run.InstanceId);
            return entity;
        }

        private static async Task<IVMachine?> GetCurrentMachine(DbgUiTool.DbgRun rm)
        {
            if (await GetCurrentRunObj(rm) is not { } runObj)
                return null;
            var hyper = Hypervisor.Default;
            IVMachine? machine;
            while ((machine = hyper.GetRunning(runObj.Entity.Id)) == null)
                await Task.Delay(50);
            return machine;
        }

        private void StopBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            if (this.GetCurrentRun() is not { } ro) return;
            if (ro.Dbg.CurrentMach is not { } machine) return;
            machine.Stop();
            StopBtn.IsEnabled = false;
            StartBtn.IsEnabled = true;
        }

        private void StartBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            if (this.GetCurrentRun() is not { } ro) return;
            if (ro.Dbg.CurrentMach is not { } machine) return;
            machine.Start();
            StartBtn.IsEnabled = false;
            StopBtn.IsEnabled = true;
        }
    }
}
