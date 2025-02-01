using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Views
{
    public partial class StackView : UserControl
    {
        public StackView()
        {
            InitializeComponent();
        }

        private void Control_OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (DataContext is not StackViewModel)
                DataContext = new StackViewModel();

            if (DataContext is StackViewModel mvm)
                mvm.Init();
        }
    }
}