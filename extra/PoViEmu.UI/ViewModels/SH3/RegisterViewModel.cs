using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.UI.ViewModels;
using MachineStateSH3 = PoViEmu.SH3.CPU.MachineState;

namespace PoViEmu.UI.ViewModels.SH3
{
    public partial class RegisterViewModel : ViewModelBase
    {
        [ObservableProperty] private MachineStateSH3 _state;

        public RegisterViewModel()
        {
            State = new()
            {
                I0 = true, I1 = true, I2 = true, I3 = true, M = true,
                Q = true, T = true, R0 = 0x10, R1 = 0x11, R2 = 0x12,
                R3 = 0x13, R4 = 0x14, R5 = 0x15, R6 = 0x16, R7 = 0x17,
                R8 = 0x18, R9 = 0x19, R10 = 0x20, R11 = 0x21, R12 = 0x22,
                R13 = 0x23, R14 = 0x24, R15 = 0x25, R0_b = 0x30, R1_b = 0x31,
                R2_b = 0x32, R3_b = 0x33, R4_b = 0x34, R5_b = 0x35, R6_b = 0x36,
                R7_b = 0x37, S = true, MD = true, PC = 0x38, PR = 0x39,
                BL = true, SSR = 0x40, dPC = 0x41, GBR = 0x42, RB = true,
                SPC = 0x43, VBR = 0x44, MACH = 0x45, MACL = 0x46
            };
        }
    }
}