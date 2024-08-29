using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.Core.Inventory;
using PoViEmu.UI.Models;
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

        private async void OnCellPointerPressed(object? sender, DataGridCellPointerPressedEventArgs e)
        {
            var grid = (DataGrid)sender!;
            var isDouble = e.PointerPressedEventArgs.ClickCount == 2;
            var repo = AppRepo.Instance;

            if (e.Cell.DataContext is AddInPlusItem ai
                && grid.ItemsSource is ObservableCollection<AddInPlusItem> ail
                && isDouble)
            {
                _ = await repo.GetCached(ai.Item);
                grid.ItemsSource = null;
                grid.ItemsSource = ail;
            }

            if (e.Cell.DataContext is SystemPlusItem si
                && grid.ItemsSource is ObservableCollection<SystemPlusItem> sil
                && isDouble)
            {
                _ = await repo.GetCached(si.Item);
                grid.ItemsSource = null;
                grid.ItemsSource = sil;
            }

            if (e.Cell.DataContext is BiosPlusItem bi
                && grid.ItemsSource is ObservableCollection<BiosPlusItem> bil
                && isDouble)
            {
                _ = await repo.GetCached(bi.Item);
                grid.ItemsSource = null;
                grid.ItemsSource = bil;
            }
        }
    }
}