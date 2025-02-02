using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.Tools;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Control_OnLoaded(object? sender, RoutedEventArgs e)
        {
            _ = this.GetContext<MainViewModel>();
        }
    }
}