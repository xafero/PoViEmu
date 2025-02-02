using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.Base.CPU;
using PoViEmu.UI.Tools;
using PoViEmu.UI.ViewModels;
using StateSH3 = PoViEmu.SH3.CPU.MachineState;

namespace PoViEmu.UI.Views
{
    public partial class RegHitView : UserControl
    {
        public static readonly StyledProperty<IState?> StateProperty =
            AvaloniaProperty.Register<RawMemView, IState?>(nameof(State));

        public IState? State
        {
            get => GetValue(StateProperty);
            set => SetValue(StateProperty, value);
        }

        public RegHitView()
        {
            InitializeComponent();
        }

        private void Control_OnLoaded(object? sender, RoutedEventArgs e)
        {
            switch (State ?? Defaults.StateSh3)
            {
                case StateSH3 sh3:
                {
                    var model = this.GetContext<RegHitViewModel>();
                    model.State = sh3;
                    break;
                }
            }
        }
    }
}