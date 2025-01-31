using Avalonia.Controls;
using Avalonia.Interactivity;
using FunDesk.ViewModels.SH3;

namespace FunDesk.Views.SH3
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