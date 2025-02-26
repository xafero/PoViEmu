using CommunityToolkit.Mvvm.ComponentModel;
using StateSH3 = PoViEmu.SH3.CPU.MachineState;
using StateI86 = PoViEmu.I186.CPU.MachineState;

namespace PoViEmu.UI.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        [ObservableProperty] private StateI86 _stateI86;

        [ObservableProperty] private StateSH3 _stateSh3;
    }
}