using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.Tools;
using PoViEmu.UI.Dbg.Core;
using PoViEmu.UI.Dbg.Models;
using PoViEmu.UI.Dbg.ViewModels;
using StateSH3 = PoViEmu.SH3.CPU.MachineState;
using StateI86 = PoViEmu.I186.CPU.MachineState;

// ReSharper disable AsyncVoidMethod
// ReSharper disable UnusedType.Global

namespace PoViEmu.UI.Dbg.Views
{
    public partial class RawMemView : UserControl
    {
        public RawMemView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (this.FindData<RunDbgViewModel>() is not { } rvm) return;
            rvm.OnInitialized += OnInitialized;
        }

        private static void OnInitialized(object? sender, InitEventArgs e)
        {
            var rvm = (RunDbgViewModel)sender!;
            switch (rvm.GetState())
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