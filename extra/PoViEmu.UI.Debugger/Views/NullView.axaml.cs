using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.Core;
using PoViEmu.UI.Dbg.ViewModels;
using PoViEmu.UI.Extensions;
using PoViEmu.UI.Tools;

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
            if (this.FindData<NullViewModel>() is { } vm)
            {
                var val = e.Value;
                var name = val.GetType().FullName;
                vm.ModelName = name;
            }
        }
    }
}