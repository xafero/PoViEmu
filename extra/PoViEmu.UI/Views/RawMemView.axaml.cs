using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.Models;
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
            for (var i = 0; i < 100; i++)
            {
                model.Lines.Add(new BytesLine("001", "2a", "....rr..df."));
                model.Lines.Add(new BytesLine("003", "3b", ".s..twqx.z."));
                model.Lines.Add(new BytesLine("005", "4c", "...123..xx."));
            }
        }

        private void RefreshContainer_OnRefreshRequested(object? sender, RefreshRequestedEventArgs e)
        {
            var deferral = e.GetDeferral();

            // TODO: Refresh List Box Items

            deferral.Complete();
        }
    }
}