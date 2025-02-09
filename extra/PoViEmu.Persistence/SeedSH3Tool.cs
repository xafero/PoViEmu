using static PoViEmu.Persistence.ValueTool;
using StateSH3 = PoViEmu.SH3.CPU.MachineState;
using FlagSH3 = PoViEmu.SH3.ISA.Flagged;

// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global

namespace PoViEmu.Persistence
{
    public static class SeedSH3Tool
    {
        public static StateSH3 Convert(this SeedSH3 e)
        {
            var item = new StateSH3();
            Convert(e, item);
            return item;
        }

        public static void Convert(this SeedSH3 e, StateSH3 i)
        {
            i.R0 = FromStr<uint>(e.R0);
            i.R1 = FromStr<uint>(e.R1);
            i.R2 = FromStr<uint>(e.R2);
            i.R3 = FromStr<uint>(e.R3);
            i.R4 = FromStr<uint>(e.R4);
            i.R5 = FromStr<uint>(e.R5);
            i.R6 = FromStr<uint>(e.R6);
            i.R7 = FromStr<uint>(e.R7);
            i.R8 = FromStr<uint>(e.R8);
            i.R9 = FromStr<uint>(e.R9);
            i.R10 = FromStr<uint>(e.R10);
            i.R11 = FromStr<uint>(e.R11);
            i.R12 = FromStr<uint>(e.R12);
            i.R13 = FromStr<uint>(e.R13);
            i.R14 = FromStr<uint>(e.R14);
            i.R15 = FromStr<uint>(e.R15);
            i.R0_b = FromStr<uint>(e.R0_b);
            i.R1_b = FromStr<uint>(e.R1_b);
            i.R2_b = FromStr<uint>(e.R2_b);
            i.R3_b = FromStr<uint>(e.R3_b);
            i.R4_b = FromStr<uint>(e.R4_b);
            i.R5_b = FromStr<uint>(e.R5_b);
            i.R6_b = FromStr<uint>(e.R6_b);
            i.R7_b = FromStr<uint>(e.R7_b);
            i.PC = FromStr<uint>(e.PC);
            i.dPC = FromStr<uint>(e.dPC);
            i.GBR = FromStr<uint>(e.GBR);
            i.VBR = FromStr<uint>(e.VBR);
            i.MACH = FromStr<uint>(e.MACH);
            i.MACL = FromStr<uint>(e.MACL);
            i.PR = FromStr<uint>(e.PR);
            i.SSR = FromStr<uint>(e.SSR);
            i.SR = FromStr<FlagSH3>(e.SR);
            i.SPC = FromStr<uint>(e.SPC);
        }
    }
}