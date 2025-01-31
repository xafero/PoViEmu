using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.ViewModels.I186;

namespace PoViEmu.UI.Views.I186
{
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        private void Control_OnLoaded(object? sender, RoutedEventArgs e)
        {
            DataContext = new RegisterViewModel();
        }
    }
}