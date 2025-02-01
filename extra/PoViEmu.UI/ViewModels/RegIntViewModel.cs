using CommunityToolkit.Mvvm.ComponentModel;
using MachineStateI86 = PoViEmu.I186.CPU.MachineState;

namespace PoViEmu.UI.ViewModels
{
    public partial class RegIntViewModel : ViewModelBase
    {
        [ObservableProperty] private MachineStateI86 _state;

        public RegIntViewModel()
        {
            State = new()
            {
                Bk0 = 0x10, Bk1 = 0x11, Bk2 = 0x12, Bk3 = 0x13,
                Bk4 = 0x14, Bk5 = 0x15, Bk6 = 0x16, Fr0 = 0x20,
                Fr1 = 0x21, Fr2 = 0x22, Fr3 = 0x23, Fr4 = 0x24,
                Fr5 = 0x25, Fr6 = 0x26, Fr7 = 0x27, Fr8 = 0x28,
                Fr9 = 0x29, Fr10 = 0x30, Fr11 = 0x31, AX = 0x3201,
                BX = 0x3302, CX = 0x3403, DX = 0x3504,
                CF = true, OF = true, DF = true, SF = true,
                AF = true, ES = 0x36, DS = 0x37, SP = 0x38,
                BP = 0x39, CS = 0x40, DI = 0x41, SI = 0x42,
                IF = true, IP = 0x43, SS = 0x44, TF = true,
                PF = true, ZF = true
            };
        }
    }
}