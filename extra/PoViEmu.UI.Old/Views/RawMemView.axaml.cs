using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Views
{
    public partial class RawMemView : UserControl
    {
        public RawMemView()
        {
            InitializeComponent();
        }

        private void Control_OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (DataContext is not RawMemViewModel)
                DataContext = new RawMemViewModel();

            if (DataContext is RawMemViewModel mvm)
                mvm.Init();
        }
    }
}