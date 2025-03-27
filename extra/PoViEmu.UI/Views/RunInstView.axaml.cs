using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.Routes;
using PoViEmu.UI.Tools;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Views
{
    public partial class RunInstView : UserControl
    {
        public RunInstView()
        {
            InitializeComponent();
        }

        private void DoExit(object? sender, RoutedEventArgs e)
        {
            this.GetRouter().GoBack();
        }

        private void DoChangeView(object? sender, RoutedEventArgs e)
        {
            if (this.FindData<RunInstViewModel>() is { } vm)
            {
                var oldState = vm.ViewIsMinimal;
                vm.ViewIsMinimal = !oldState;
            }
        }
    }
}