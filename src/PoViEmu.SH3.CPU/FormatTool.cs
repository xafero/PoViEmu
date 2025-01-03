using System;

namespace PoViEmu.SH3.CPU
{
    public static class FormatTool
    {
        public static string ToRegisterString(this MachineState s, string? rawSep = null)
        {
            var sep = rawSep ?? Environment.NewLine;
            return $"R0={s.R0:x}{sep}R1={s.R0:x}{sep}R2={s.R2:x}{sep}R3={s.R3:x}{sep}" +
                   $"R4={s.R4:x}{sep}R5={s.R5:x}{sep}R6={s.R6:x}{sep}R7={s.R7:x}{sep}" +
                   $"R8={s.R8:x}{sep}R9={s.R9:x}{sep}R10={s.R10:x}{sep}R11={s.R11:x}{sep}" +
                   $"R12={s.R12:x}{sep}R13={s.R13:x}{sep}R14={s.R14:x}{sep}R15={s.R15:x}{sep}" +
                   $"MACH={s.MACH:x}{sep}MACL={s.MACL:x}{sep}GBR={s.GBR:x}{sep}" +
                   $"VBR={s.VBR:x}{sep}PC={s.PC:x}{sep}SSR={s.SSR:x}{sep}SPC={s.SPC:x}{sep}" +
                   $"T={(s.T ? 1 : 0)}{sep}S={(s.S ? 1 : 0)}{sep}" +
                   $"I0={(s.I0 ? 1 : 0)}{sep}I1={(s.I1 ? 1 : 0)}{sep}" +
                   $"I2={(s.I2 ? 1 : 0)}{sep}I3={(s.I3 ? 1 : 0)}{sep}" +
                   $"Q={(s.Q ? 1 : 0)}{sep}M={(s.M ? 1 : 0)}{sep}" +
                   $"BL={(s.BL ? 1 : 0)}{sep}RB={(s.RB ? 1 : 0)}{sep}" +
                   $"MD={(s.MD ? 1 : 0)}{sep}" +
                   $"R0b={s.R0_b:x}{sep}R1b={s.R0_b:x}{sep}R2b={s.R2_b:x}{sep}R3b={s.R3_b:x}{sep}" +
                   $"R4b={s.R4_b:x}{sep}R5b={s.R5_b:x}{sep}R6b={s.R6_b:x}{sep}R7b={s.R7_b:x}{sep}";
        }
    }
}