using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Views
{
    public partial class AddInView : UserControl
    {
        public AddInView()
        {
            InitializeComponent();
        }

        private void Control_OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (DataContext is not AddInViewModel)
                DataContext = new AddInViewModel();

            if (DataContext is AddInViewModel mvm)
                mvm.Load();
        }
    }
}