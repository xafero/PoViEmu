using Avalonia.Controls;
using Avalonia.Interactivity;

// ReSharper disable AsyncVoidMethod
// ReSharper disable UnusedType.Global

namespace PoViEmu.UI.Dbg.Views
{
    public partial class UnassView : UserControl
    {
        public UnassView()
        {
            InitializeComponent();
        }

        private async void OnLoaded(object? sender, RoutedEventArgs e)
        {
        }

        private void RefreshContainer_OnRefreshRequested(object? sender, RefreshRequestedEventArgs e)
        {
            var deferral = e.GetDeferral();

            // TODO: Refresh List Box Items

            deferral.Complete();
        }
    }
}