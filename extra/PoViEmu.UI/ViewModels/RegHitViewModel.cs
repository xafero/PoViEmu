using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.UI.Tools;
using MachineStateSH3 = PoViEmu.SH3.CPU.MachineState;

namespace PoViEmu.UI.ViewModels
{
    public partial class RegHitViewModel : ViewModelBase
    {
        [ObservableProperty] private MachineStateSH3 _state;

        public RegHitViewModel()
        {
            State = Defaults.StateSh3;
        }
    }
}