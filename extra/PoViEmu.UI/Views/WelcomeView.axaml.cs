using Avalonia.Controls;
using Avalonia.Interactivity;
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
            this.GetRouter().Push<TemplViewModel>();
        }
    }
}