using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Views
{
    public partial class UnassView : UserControl
    {
        public UnassView()
        {
            InitializeComponent();
        }

        private void Control_OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (DataContext is not UnassViewModel)
                DataContext = new UnassViewModel();

            if (DataContext is UnassViewModel mvm)
                mvm.Init();
        }
    }
}