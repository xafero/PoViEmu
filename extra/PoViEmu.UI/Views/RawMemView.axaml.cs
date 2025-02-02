using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.Base.CPU;
using PoViEmu.UI.Tools;
using PoViEmu.UI.ViewModels;
using StateSH3 = PoViEmu.SH3.CPU.MachineState;
using StateI86 = PoViEmu.I186.CPU.MachineState;

namespace PoViEmu.UI.Views
{
    public partial class RawMemView : UserControl
    {
        public static readonly StyledProperty<IState?> StateProperty =
            AvaloniaProperty.Register<RawMemView, IState?>(nameof(State));

        public IState? State
        {
            get => GetValue(StateProperty);
            set => SetValue(StateProperty, value);
        }

        public RawMemView()
        {
            InitializeComponent();
        }

        private void Control_OnLoaded(object? sender, RoutedEventArgs e)
        {
            switch (State ?? Defaults.StateI86)
            {
                case StateI86 x86:
                {
                    var model = this.GetContext<MemIntViewModel>();
                    model.Read(x86);
                    break;
                }
                case StateSH3 sh3:
                {
                    var model = this.GetContext<MemHitViewModel>();
                    model.Read(sh3);
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