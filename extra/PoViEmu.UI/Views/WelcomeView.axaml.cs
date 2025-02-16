using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using PoViEmu.UI.Tools;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Views
{
    public partial class WelcomeView : UserControl
    {
        public WelcomeView()
        {
            InitializeComponent();
        }

        private void OnNextClick(object? sender, RoutedEventArgs e)
        {
            if (this.FindData<MainViewModel>() is { } mv)
            {
                mv.CurrentView = null;
            }
        }
    }
}