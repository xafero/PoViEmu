using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.UI.Tools;
using MachineStateI86 = PoViEmu.I186.CPU.MachineState;

namespace PoViEmu.UI.ViewModels
{
    public partial class RegIntViewModel : ViewModelBase
    {
        [ObservableProperty] private MachineStateI86 _state;

        public RegIntViewModel()
        {
            State = Defaults.StateI86;
        }
    }
}