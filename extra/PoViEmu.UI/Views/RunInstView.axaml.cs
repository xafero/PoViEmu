using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.Routes;

namespace PoViEmu.UI.Views
{
    public partial class RunInstView : UserControl
    {
        public RunInstView()
        {
            InitializeComponent();
        }

        private void OnExit(object? sender, RoutedEventArgs e)
        {
            this.GetRouter().GoBack();
        }
    }
}