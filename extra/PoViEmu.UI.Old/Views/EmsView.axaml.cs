using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Views
{
    public partial class EmsView : UserControl
    {
        public EmsView()
        {
            InitializeComponent();
        }

        private void Control_OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (DataContext is not EmsViewModel)
                DataContext = new EmsViewModel();

            if (DataContext is EmsViewModel mvm)
                mvm.Init();
        }
    }
}