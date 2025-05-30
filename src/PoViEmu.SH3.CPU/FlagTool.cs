using Fl = PoViEmu.SH3.ISA.Flagged;

namespace PoViEmu.SH3.CPU
{
    public static class FlagTool
    {
        public static void SetFlags(this MachineState m, Fl value)
        {
            m.M = value.HasFlag(Fl.M_bit);
            m.Q = value.HasFlag(Fl.Q_bit);
            m.S = value.HasFlag(Fl.S_bit);
            m.T = value.HasFlag(Fl.T_bit);
            m.I0 = value.HasFlag(Fl.I0);
            m.I1 = value.HasFlag(Fl.I1);
            m.I2 = value.HasFlag(Fl.I2);
            m.I3 = value.HasFlag(Fl.I3);
            m.BL = value.HasFlag(Fl.Block_bit);
            m.RB = value.HasFlag(Fl.Bank_bit);
            m.MD = value.HasFlag(Fl.Mode_bit);
        }
    }
}