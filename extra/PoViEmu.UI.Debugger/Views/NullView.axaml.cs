using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.Core;
using PoViEmu.UI.Extensions;

namespace PoViEmu.UI.Dbg.Views
{
    public partial class NullView : UserControl
    {
        public NullView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
            ExtPoints.Instance.ViewChanged += OnViewChanged;
        }

        private void OnViewChanged(object? sender, GenArgs<IViewModelBase> e)
        {
            // TODO
        }
    }
}