using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.Tools;
using PoViEmu.UI.Dbg.Core;
using PoViEmu.UI.Dbg.ViewModels;
using StateSH3 = PoViEmu.SH3.CPU.MachineState;
using StateI86 = PoViEmu.I186.CPU.MachineState;

// ReSharper disable AsyncVoidMethod
// ReSharper disable UnusedType.Global

namespace PoViEmu.UI.Dbg.Views
{
    public partial class StackView : UserControl
    {
        public StackView()
        {
            InitializeComponent();
        }

        private async void OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (this.FindData<RunDbgViewModel>() is not { } rvm) return;
            await rvm.Await(x => x.CurrentMach != null);
            var state = rvm.GetState();
            switch (state)
            {
                case StateI86 x86:
                {
                    StackTool.Read(rvm, x86);
                    break;
                }
                case StateSH3 sh3:
                {
                    StackTool.Read(rvm, sh3);
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