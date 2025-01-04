using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Views
{
    public partial class HandheldView : UserControl
    {
        public HandheldView()
        {
            InitializeComponent();
        }

        private void Control_OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (DataContext is HandheldViewModel mvm)
                mvm.Title = nameof(PoViEmu);
        }
    }
}