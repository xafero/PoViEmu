using PoViEmu.Hyper;
using PoViEmu.UI.Core;
using PoViEmu.UI.Routes;
using PoViEmu.UI.Tools;
using PoViEmu.UI.ViewModels;
using PoViEmu.UI.Dbg.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using M186 = PoViEmu.I186.CPU.MachineState;
using MSh3 = PoViEmu.SH3.CPU.MachineState;

namespace PoViEmu.UI.Dbg.ViewModels
{
    public partial class RunDbgViewModel : ViewModelBase, IRoutable
    {
        [ObservableProperty] private RunInstViewModel? _base;

        [ObservableProperty] private IVMachine? _currentMach;

        [ObservableProperty] private RunUtil.RunObj? _currentInfo;

        partial void OnCurrentMachChanged(IVMachine? value)
        {
            switch (value?.State)
            {
                case M186 m186:
                    StateN = m186;
                    StateH = null;
                    break;
                case MSh3 mSh3:
                    StateN = null;
                    StateH = mSh3;
                    break;
            }
        }

        [ObservableProperty] private M186? _stateN;
        [ObservableProperty] private MSh3? _stateH;
        [ObservableProperty] private ObservableCollection<BytesLine> _memLines = new();
        [ObservableProperty] private ObservableCollection<BytesLine> _disLines = new();
    }
}