using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.Tools;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Views
{
    public partial class RegHitView : UserControl
    {
        public RegHitView()
        {
            InitializeComponent();
        }

        private void Control_OnLoaded(object? sender, RoutedEventArgs e)
        {
            _ = this.GetContext<RegHitViewModel>();
        }
    }
}