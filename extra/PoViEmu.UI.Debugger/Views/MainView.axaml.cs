using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.Dbg.Core;
using PoViEmu.UI.Dbg.ViewModels;
using PoViEmu.UI.Tools;

namespace PoViEmu.UI.Dbg.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void Control_OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (this.FindData<MainViewModel>() is { } vm)
            {
                var model = Setup.Null;
                vm.CurrentView = model;
            }
        }
    }
}