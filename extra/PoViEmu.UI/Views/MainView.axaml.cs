using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.Tools;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void Control_OnLoaded(object? sender, RoutedEventArgs e)
        {
            var model = this.GetContext<MainViewModel>();
            model.StateSh3 = Defaults.StateSh3;
            model.StateI86 = Defaults.StateI86;
        }
    }
}