using System.Text;
using PoViEmu.Core.Decoding;
using System.Collections.Generic;
using System.Text;
using PoViEmu.Core.Hardware;

namespace PoViEmu.Core.Hardware
{
    public static class DebugTool
    {
        public static string ToFlagDebug(this Flagged fl)
        {
            var bld = new StringBuilder();
            bld.Append($"{(fl.HasFlag(Flagged.Overflow) ? "OV" : "NV")} ");
            bld.Append($"{(fl.HasFlag(Flagged.Direction) ? "DN" : "UP")} ");
            bld.Append($"{(fl.HasFlag(Flagged.Interrupt) ? "EI" : "DI")} ");
            bld.Append($"{(fl.HasFlag(Flagged.Sign) ? "NG" : "PL")} ");
            bld.Append($"{(fl.HasFlag(Flagged.Zero) ? "ZR" : "NZ")} ");
            bld.Append($"{(fl.HasFlag(Flagged.Auxiliary) ? "AC" : "NA")} ");
            bld.Append($"{(fl.HasFlag(Flagged.Parity) ? "PE" : "PO")} ");
            bld.Append($"{(fl.HasFlag(Flagged.Carry) ? "CY" : "NC")} ");
            return bld.ToString().Trim();
        }

        public static IEnumerable<string> ToRegDebugLin(this MachineState m)
        {
            yield return $"AX={m.AX.Format(false)} " +
                         $"BX={m.BX.Format(false)} " +
                         $"CX={m.CX.Format(false)} " +
                         $"DX={m.DX.Format(false)} " +
                         $"SP={m.SP.Format(false)} " +
                         $"BP={m.BP.Format(false)} " +
                         $"SI={m.SI.Format(false)} " +
                         $"DI={m.DI.Format(false)}";
            yield return $"DS={m.DS.Format(false)} " +
                         $"ES={m.ES.Format(false)} " +
                         $"SS={m.SS.Format(false)} " +
                         $"CS={m.CS.Format(false)} " +
                         $"IP={m.IP.Format(false)} " +
                         $"{ToFlagDebug(m.F)}";
        }
    }
}