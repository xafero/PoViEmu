using System;

namespace PoViEmu.I186.CPU
{
    public static class FormatTool
    {
        public static string ToRegisterString(this MachineState s, string? rawSep = null)
        {
            var sep = rawSep ?? Environment.NewLine;
            return $"AX={s.AX:x4}{sep}BX={s.BX:x4}{sep}CX={s.CX:x4}{sep}DX={s.DX:x4}{sep}" +
                   $"SI={s.SI:x4}{sep}DI={s.DI:x4}{sep}DS={s.DS:x4}{sep}ES={s.ES:x4}{sep}" +
                   $"SS={s.SS:x4}{sep}SP={s.SP:x4}{sep}BP={s.BP:x4}{sep}CS={s.CS:x4}{sep}" +
                   $"IP={s.IP:x4}{sep}CF={(s.CF ? 1 : 0)}{sep}ZF={(s.ZF ? 1 : 0)}{sep}" +
                   $"SF={(s.SF ? 1 : 0)}{sep}DF={(s.DF ? 1 : 0)}{sep}" +
                   $"IF={(s.IF ? 1 : 0)}{sep}OF={(s.OF ? 1 : 0)}{sep}" +
                   $"PF={(s.PF ? 1 : 0)}{sep}AF={(s.AF ? 1 : 0)}{sep}" +
                   $"TF={(s.TF ? 1 : 0)}{sep}" +
                   $"B0={s.Bk0:x4}{sep}B1={s.Bk1:x4}{sep}B2={s.Bk2:x4}{sep}" +
                   $"B3={s.Bk3:x4}{sep}B4={s.Bk4:x4}{sep}B5={s.Bk5:x4}{sep}" +
                   $"B6={s.Bk6:x4}{sep}F0={s.Fr0:x4}{sep}F1={s.Fr1:x4}{sep}" +
                   $"F2={s.Fr2:x4}{sep}F3={s.Fr3:x4}{sep}F4={s.Fr4:x4}{sep}" +
                   $"F5={s.Fr5:x4}{sep}F6={s.Fr6:x4}{sep}F7={s.Fr7:x4}{sep}" +
                   $"F8={s.Fr8:x4}{sep}F9={s.Fr9:x4}{sep}F10={s.Fr10:x4}{sep}" +
                   $"F11={s.Fr11:x4}{sep}";
        }
    }
}