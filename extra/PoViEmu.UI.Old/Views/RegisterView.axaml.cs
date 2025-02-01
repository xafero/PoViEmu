using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Views
{
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        private void Control_OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (DataContext is not RegisterViewModel)
                DataContext = new RegisterViewModel();

            if (DataContext is RegisterViewModel mvm)
                mvm.Init();
        }
    }
}