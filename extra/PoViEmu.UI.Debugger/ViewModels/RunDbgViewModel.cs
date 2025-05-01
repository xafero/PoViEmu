using PoViEmu.Hyper;
using PoViEmu.UI.Core;
using PoViEmu.UI.Routes;
using PoViEmu.UI.Tools;
using PoViEmu.UI.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PoViEmu.UI.Dbg.ViewModels
{
    public partial class RunDbgViewModel : ViewModelBase, IRoutable
    {
        [ObservableProperty] private RunInstViewModel? _base;

        [ObservableProperty] private IVMachine? _currentMach;

        [ObservableProperty] private RunUtil.RunObj? _currentInfo;
    }
}