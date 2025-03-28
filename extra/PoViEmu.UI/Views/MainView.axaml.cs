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
        }

        private void Control_OnSizeChanged(object? sender, SizeChangedEventArgs e)
        {
            if (this.FindData<MainViewModel>() is { } vm)
            {
                var size = e.NewSize;
                if (size.Height >= size.Width)
                    vm.Orientation = Orientation.Portrait;
                else
                    vm.Orientation = Orientation.Landscape;
            }
        }
    }
}