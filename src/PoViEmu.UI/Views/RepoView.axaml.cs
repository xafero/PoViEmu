using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Views
{
    public partial class RepoView : UserControl
    {
        public RepoView()
        {
            InitializeComponent();
        }

        private async void Control_OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (DataContext is not RepoViewModel)
                DataContext = new RepoViewModel();

            if (DataContext is RepoViewModel mvm)
                await mvm.Init();
        }
    }
}