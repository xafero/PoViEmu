namespace PoViEmu.SH3.ISA
{
    public static class FlagTool
    {
        public static bool Check(this Flagged flag, ref Flagged value)
        {
            return (value & flag) == flag;
        }

        public static Flagged Add(this Flagged flag, Flagged value, bool on)
        {
            return on ? value | flag : value & ~flag;
        }
    }
}