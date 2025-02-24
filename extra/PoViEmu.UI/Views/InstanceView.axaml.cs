using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.Tools;

namespace PoViEmu.UI.Views
{
    public partial class InstanceView : UserControl
    {
        public InstanceView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
        }

        private void OnNextClick(object? sender, RoutedEventArgs e)
        {
            this.GetRouter().Push<IRoutable>(default);
        }
    }
}