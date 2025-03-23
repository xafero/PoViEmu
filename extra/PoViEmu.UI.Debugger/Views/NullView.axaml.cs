using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.UI.Core;
using PoViEmu.UI.Extensions;
using PoViEmu.UI.ViewModels;
using PoViEmu.UI.Dbg.Core;
using PoViEmu.UI.Dbg.ViewModels;
using DebugModel = PoViEmu.UI.Dbg.ViewModels.MainViewModel;
using DebugView = PoViEmu.UI.Dbg.Views.MainView;

namespace PoViEmu.UI.Dbg.Views
{
    public partial class NullView : UserControl
    {
        private DebugModel _modelRef;

        public NullView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (Parent?.Parent is DebugView { DataContext: DebugModel ctx })
                _modelRef = ctx;
            ExtPoints.Instance.ViewChanged += OnViewChanged;
        }

        private void OnViewChanged(object? sender, GenArgs<IViewModelBase> e)
        {
            var model = e.Value;
            if (model is RunInstViewModel rim)
            {
                _modelRef.CurrentView = new RunDbgViewModel { Base = rim };
                return;
            }
            _modelRef.CurrentView = Setup.Null;
        }
    }
}