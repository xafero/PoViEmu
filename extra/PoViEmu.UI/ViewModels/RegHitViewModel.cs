using CommunityToolkit.Mvvm.ComponentModel;
using MachineStateSH3 = PoViEmu.SH3.CPU.MachineState;

namespace PoViEmu.UI.ViewModels
{
    public partial class RegHitViewModel : ViewModelBase
    {
        [ObservableProperty] private MachineStateSH3 _state;
    }
}