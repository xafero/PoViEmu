using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.Tools;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Views
{
    public partial class RawMemView : UserControl
    {
        public RawMemView()
        {
            InitializeComponent();
        }

        private void Control_OnLoaded(object? sender, RoutedEventArgs e)
        {
            var model = this.GetContext<RawMemViewModel>();
            var state = Defaults.StateI86;
            var seg = state.DS;
            var off = state.SI;
            var bytes = state.ReadMemory(seg, off, 512);
            model.Read(off, bytes.ToArray());
        }

        private void RefreshContainer_OnRefreshRequested(object? sender, RefreshRequestedEventArgs e)
        {
            var deferral = e.GetDeferral();

            // TODO: Refresh List Box Items

            deferral.Complete();
        }
    }
}