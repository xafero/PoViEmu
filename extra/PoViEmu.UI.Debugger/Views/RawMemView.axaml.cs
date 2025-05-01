using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.Tools;
using PoViEmu.Base.CPU;
using PoViEmu.UI.Dbg.Core;
using PoViEmu.UI.Dbg.ViewModels;
using StateSH3 = PoViEmu.SH3.CPU.MachineState;
using StateI86 = PoViEmu.I186.CPU.MachineState;

// ReSharper disable AsyncVoidMethod

namespace PoViEmu.UI.Dbg.Views
{
    public partial class RawMemView : UserControl
    {
        private static IState? GetState(RunDbgViewModel rvm)
        {
            if (rvm.StateH is { } stateH) return stateH;
            if (rvm.StateN is { } stateN) return stateN;
            return null;
        }

        public RawMemView()
        {
            InitializeComponent();
        }

        private async void OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (this.FindData<RunDbgViewModel>() is not { } rvm) return;
            await rvm.Await(x => x.CurrentMach != null, 50);
            var state = GetState(rvm);
            switch (state)
            {
                case StateI86 x86:
                {
                    rvm.Read(x86);
                    break;
                }
                case StateSH3 sh3:
                {
                    rvm.Read(sh3);
                    break;
                }
            }
        }

        private void RefreshContainer_OnRefreshRequested(object? sender, RefreshRequestedEventArgs e)
        {
            var deferral = e.GetDeferral();

            // TODO: Refresh List Box Items

            deferral.Complete();
        }
    }
}