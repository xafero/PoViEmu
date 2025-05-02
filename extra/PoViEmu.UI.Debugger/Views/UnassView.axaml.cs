using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.Tools;
using PoViEmu.UI.Dbg.Core;
using PoViEmu.UI.Dbg.Models;
using PoViEmu.UI.Dbg.ViewModels;
using PoViEmu.UI.Dbg.Unass;
using StateSH3 = PoViEmu.SH3.CPU.MachineState;
using StateI86 = PoViEmu.I186.CPU.MachineState;

// ReSharper disable AsyncVoidMethod
// ReSharper disable UnusedType.Global

namespace PoViEmu.UI.Dbg.Views
{
    public partial class UnassView : UserControl
    {
        private readonly UnassInt _uai = new();
        private readonly UnassHit _uah = new();

        public UnassView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (this.FindData<RunDbgViewModel>() is not { } rvm) return;
            rvm.OnInitialized += OnInitialized;
        }

        private void OnInitialized(object? sender, InitEventArgs e)
        {
            var rvm = (RunDbgViewModel)sender!;
            switch (rvm.GetState())
            {
                case StateI86 x86:
                {
                    _uai.Read(rvm, x86);
                    break;
                }
                case StateSH3 sh3:
                {
                    _uah.Read(rvm, sh3);
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