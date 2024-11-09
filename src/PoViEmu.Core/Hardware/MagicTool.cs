namespace PoViEmu.Core.Hardware
{
    public static class MagicTool
    {
        public static void DoDecimalAdjust(this MachineState m)
        {
            var (al, af, cf) = DoDecimalAdjust(m.AL, m.AF, m.CF);
            m.AL = al;
            m.AF = af;
            m.CF = cf;
        }

        private static (byte al, bool af, bool cf) DoDecimalAdjust(byte al,bool af, bool cf )
        {
            var oldAl = al;
            if ((al & 0x0F) > 9 || af)
            {
                al += 0x06;
                af = true;
            }
            al &= 0xFF;
            if (oldAl > 0x99 || cf)
            {
                al += 0x60;
                cf = true;
            }
            al &= 0xFF;
            return (al, af, cf);
        }
    }
}