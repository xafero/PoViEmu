using PoViEmu.UI.Core;
using PoViEmu.UI.Routes;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PoViEmu.UI.Dbg.ViewModels
{
    public partial class MainViewModel : ViewModelBase, IRoutable
    {
        [ObservableProperty] private IViewModelBase _currentView;
    }
}