using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.Models;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Views
{
    public partial class UnassView : UserControl
    {
        public UnassView()
        {
            InitializeComponent();
        }

        private void Control_OnLoaded(object? sender, RoutedEventArgs e)
        {
            DataContext = new UnassViewModel();

            if (DataContext is UnassViewModel uvm)
            {
                for (var i = 0; i < 100; i++)
                {
                    uvm.Lines.Add(new AssemblyLine("001", "2a", "mov ax,1"));
                    uvm.Lines.Add(new AssemblyLine("003", "3b", "mov 4,bx"));
                    uvm.Lines.Add(new AssemblyLine("005", "4c", "add cx,13"));
                }
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