using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Views
{
    public partial class MinimalView : UserControl
    {
        public MinimalView()
        {
            InitializeComponent();
        }

        private void Control_OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (DataContext is MinimalViewModel mvm)
                mvm.Title = nameof(PoViEmu);
        }
    }
}