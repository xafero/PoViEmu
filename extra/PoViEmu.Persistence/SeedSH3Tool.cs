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
            i.dPC = FromStr<uint?>(e.dPC);
            i.GBR = FromStr<uint>(e.GBR);
            i.VBR = FromStr<uint>(e.VBR);
            i.MACH = FromStr<uint>(e.MACH);
            i.MACL = FromStr<uint>(e.MACL);
            i.PR = FromStr<uint>(e.PR);
            i.SSR = FromStr<uint>(e.SSR);
            i.SR = FromStr<FlagSH3>(e.SR);
            i.SPC = FromStr<uint>(e.SPC);
        }

        public static SeedSH3 Convert(this StateSH3 e)
        {
            var item = new SeedSH3();
            Convert(e, item);
            return item;
        }

        public static void Convert(this StateSH3 e, SeedSH3 i)
        {
            i.R0 = ToStr<uint>(e.R0);
            i.R1 = ToStr<uint>(e.R1);
            i.R2 = ToStr<uint>(e.R2);
            i.R3 = ToStr<uint>(e.R3);
            i.R4 = ToStr<uint>(e.R4);
            i.R5 = ToStr<uint>(e.R5);
            i.R6 = ToStr<uint>(e.R6);
            i.R7 = ToStr<uint>(e.R7);
            i.R8 = ToStr<uint>(e.R8);
            i.R9 = ToStr<uint>(e.R9);
            i.R10 = ToStr<uint>(e.R10);
            i.R11 = ToStr<uint>(e.R11);
            i.R12 = ToStr<uint>(e.R12);
            i.R13 = ToStr<uint>(e.R13);
            i.R14 = ToStr<uint>(e.R14);
            i.R15 = ToStr<uint>(e.R15);
            i.R0_b = ToStr<uint>(e.R0_b);
            i.R1_b = ToStr<uint>(e.R1_b);
            i.R2_b = ToStr<uint>(e.R2_b);
            i.R3_b = ToStr<uint>(e.R3_b);
            i.R4_b = ToStr<uint>(e.R4_b);
            i.R5_b = ToStr<uint>(e.R5_b);
            i.R6_b = ToStr<uint>(e.R6_b);
            i.R7_b = ToStr<uint>(e.R7_b);
            i.PC = ToStr<uint>(e.PC);
            i.dPC = ToStr<uint?>(e.dPC);
            i.GBR = ToStr<uint>(e.GBR);
            i.VBR = ToStr<uint>(e.VBR);
            i.MACH = ToStr<uint>(e.MACH);
            i.MACL = ToStr<uint>(e.MACL);
            i.PR = ToStr<uint>(e.PR);
            i.SSR = ToStr<uint>(e.SSR);
            i.SR = ToStr<FlagSH3>(e.SR);
            i.SPC = ToStr<uint>(e.SPC);
        }
    }
}