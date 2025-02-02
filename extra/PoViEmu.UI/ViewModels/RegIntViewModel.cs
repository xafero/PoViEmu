using CommunityToolkit.Mvvm.ComponentModel;
using MachineStateI86 = PoViEmu.I186.CPU.MachineState;

namespace PoViEmu.UI.ViewModels
{
    public partial class RegIntViewModel : ViewModelBase
    {
        [ObservableProperty] private MachineStateI86 _state;
    }
}