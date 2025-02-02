using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.Base.CPU;
using PoViEmu.UI.Tools;
using PoViEmu.UI.ViewModels;
using StateI86 = PoViEmu.I186.CPU.MachineState;

namespace PoViEmu.UI.Views
{
    public partial class RegIntView : UserControl
    {
        public static readonly StyledProperty<IState?> StateProperty =
            AvaloniaProperty.Register<RawMemView, IState?>(nameof(State));

        public IState? State
        {
            get => GetValue(StateProperty);
            set => SetValue(StateProperty, value);
        }

        public RegIntView()
        {
            InitializeComponent();
        }

        private void Control_OnLoaded(object? sender, RoutedEventArgs e)
        {
            switch (State ?? Defaults.StateI86)
            {
                case StateI86 x86:
                {
                    var model = this.GetContext<RegIntViewModel>();
                    model.State = x86;
                    break;
                }
            }
        }
    }
}