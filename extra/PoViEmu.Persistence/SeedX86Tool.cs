using static PoViEmu.Workbook.ValueTool;
using StateX86 = PoViEmu.I186.CPU.MachineState;
using FlagX86 = PoViEmu.I186.ISA.Flagged;

// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global

namespace PoViEmu.Workbook
{
    public static class SeedX86Tool
    {
        public static StateX86 Convert(this SeedX86 e)
        {
            var item = new StateX86();
            Convert(e, item);
            return item;
        }

        public static void Convert(this SeedX86 e, StateX86 i)
        {
            i.AX = FromStr<ushort>(e.AX);
            i.BX = FromStr<ushort>(e.BX);
            i.CX = FromStr<ushort>(e.CX);
            i.DX = FromStr<ushort>(e.DX);
            i.BP = FromStr<ushort>(e.BP);
            i.SP = FromStr<ushort>(e.SP);
            i.IP = FromStr<ushort>(e.IP);
            i.SI = FromStr<ushort>(e.SI);
            i.DI = FromStr<ushort>(e.DI);
            i.F = FromStr<FlagX86>(e.F);
            i.CS = FromStr<ushort>(e.CS);
            i.DS = FromStr<ushort>(e.DS);
            i.ES = FromStr<ushort>(e.ES);
            i.SS = FromStr<ushort>(e.SS);
            i.Bk0 = FromStr<ushort>(e.Bk0);
            i.Bk1 = FromStr<ushort>(e.Bk1);
            i.Bk2 = FromStr<ushort>(e.Bk2);
            i.Bk3 = FromStr<ushort>(e.Bk3);
            i.Bk4 = FromStr<ushort>(e.Bk4);
            i.Bk5 = FromStr<ushort>(e.Bk5);
            i.Bk6 = FromStr<ushort>(e.Bk6);
            i.Fr0 = FromStr<ushort>(e.Fr0);
            i.Fr1 = FromStr<ushort>(e.Fr1);
            i.Fr2 = FromStr<ushort>(e.Fr2);
            i.Fr3 = FromStr<ushort>(e.Fr3);
            i.Fr4 = FromStr<ushort>(e.Fr4);
            i.Fr5 = FromStr<ushort>(e.Fr5);
            i.Fr6 = FromStr<ushort>(e.Fr6);
            i.Fr7 = FromStr<ushort>(e.Fr7);
            i.Fr8 = FromStr<ushort>(e.Fr8);
            i.Fr9 = FromStr<ushort>(e.Fr9);
            i.Fr10 = FromStr<ushort>(e.Fr10);
            i.Fr11 = FromStr<ushort>(e.Fr11);
        }
    }
}