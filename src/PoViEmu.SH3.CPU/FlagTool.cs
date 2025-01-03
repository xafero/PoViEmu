using PoViEmu.SH3.ISA;

namespace PoViEmu.SH3.CPU
{
    public static class FlagTool
    {
        public static void SetFlags(this MachineState m, Flagged value)
        {
            m.M = value.HasFlag(Flagged.M_bit);
            m.Q = value.HasFlag(Flagged.Q_bit);
            m.S = value.HasFlag(Flagged.S_bit);
            m.T = value.HasFlag(Flagged.T_bit);
        }
    }
}