using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Views
{
    public partial class SimCtrlView : UserControl
    {
        public SimCtrlView()
        {
            InitializeComponent();
        }

        private void Control_OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (DataContext is not SimCtrlViewModel)
                DataContext = new SimCtrlViewModel();

            if (DataContext is SimCtrlViewModel mvm)
                mvm.Init();
        }
    }
}