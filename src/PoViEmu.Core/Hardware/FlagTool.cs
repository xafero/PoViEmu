namespace PoViEmu.Core.Hardware
{
    public static class FlagTool
    {
        public static void CalcParityFlag(this IFlagged x, int result)
        {
            var num = (byte)(result & 0xff);
            byte total;
            for (total = 0; num > 0; total++) 
                num &= (byte)(num - 1);
            x.ParityFlag = total % 2 == 0;
        }

        public static void CalcSignFlag(this IFlagged x, bool isByte, int result)
        {
            if (isByte)
                x.SignFlag = (result & 0x80) == 0x80;
            else
                x.SignFlag = (result & 0x8000) == 0x8000;
        }

        public static void CalcZeroFlag(this IFlagged x, bool isByte, int result)
        {
            if (isByte)
                x.ZeroFlag = (result & 0xff) == 0;
            else
                x.ZeroFlag = (result & 0xffff) == 0;
        }

        public static void CalcAuxCarryFlag(this IFlagged x, int src, int dst)
        {
            var result = src + dst;
            x.AuxCarryFlag = ((src ^ dst ^ result) & 0x10) == 0x10;
        }

        public static void CalcOverflowSubtract(this IFlagged x, bool isByte, int src, int dest)
        {
            var result = dest - src;
            if (isByte)
                x.OverflowFlag = ((result ^ dest) & (src ^ dest) & 0x80) == 0x80;
            else
                x.OverflowFlag = ((result ^ dest) & (src ^ dest) & 0x8000) == 0x8000;
        }

        public static void CalcOverflowFlag(this IFlagged x, bool isByte, int src, int dest)
        {
            var result = src + dest;
            if (isByte)
                x.OverflowFlag = ((result ^ src) & (result ^ dest) & 0x80) == 0x80;
            else
                x.OverflowFlag = ((result ^ src) & (result ^ dest) & 0x8000) == 0x8000;
        }

        public static void CalcCarryFlag(this IFlagged x, bool isByte, int result)
        {
            if (isByte)
                x.CarryFlag = (ushort)result > 0xff;
            else
            {
                x.CarryFlag = (uint)result > 0xffff;
            }
        }
    }

    public interface IFlagged
    {
        bool CarryFlag { set; }
        bool AuxCarryFlag { set; }
        bool ParityFlag { set; }
        bool SignFlag { set; }
        bool ZeroFlag { set; }
        bool OverflowFlag { set; }
    }
}