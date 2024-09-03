using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PoViEmu.Common;

namespace PoViEmu.Core.Decoding
{
    public static class FormatExt
    {
        public static string ToMemoryString(this MachineState s, string? rawSep = null)
        {
            var sep = rawSep ?? Environment.NewLine;
            var bld = new StringBuilder();
            const int step = 16;
            foreach (var seg in s.Memory)
            foreach (var off in seg.Value)
            {
                var i = 0;
                foreach (var line in off.Value.SplitIt(step))
                {
                    var text = $"{seg.Key:X4}:{off.Key + (i * step):X4}   " +
                               $"{string.Join(" ", line.Select(b => $"{b:X2}"))}   " +
                               $"{line.DecodeChars()}";
                    bld.Append(text);
                    bld.Append(sep);
                    i++;
                }
            }
            return bld.ToString();
        }

        public static string ToCodeString(this MachineState s, string? rawSep = null)
        {
            var sep = rawSep ?? Environment.NewLine;
            var bld = new StringBuilder();
            foreach (var seg in s.Memory)
            foreach (var off in seg.Value)
            {
                using var mem = new MemoryStream(off.Value.ToArray());
                using var reader = new MemCodeReader(mem);
                foreach (var item in reader.Decode(off.Key))
                {
                    var text = item.ToString($"{seg.Key:X4}:");
                    bld.Append(text);
                    bld.Append(sep);
                }
            }
            return bld.ToString();
        }

        public static string ToStackString(this MachineState s, string? rawSep = null)
        {
            var sep = rawSep ?? Environment.NewLine;
            var bld = new StringBuilder();
            foreach (var (_, vals) in s.GetStackVals())
            {
                foreach (var t in vals)
                {
                    var text = $"SS:{t.addr:X4}   {t.val:x4}";
                    bld.Append(text);
                    bld.Append(sep);
                }
            }
            return bld.ToString();
        }

        public static IEnumerable<(ushort o, IEnumerable<(ushort addr, ushort val)> v)>
            GetStackVals(this MachineState s)
            => s.Stack.Select(item =>
                (off: item.Key, vals: item.Value.Select((val, i) =>
                    (addr: (ushort)(item.Key + i * 2), val))));

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
                   $"B0={s.Bank0:x4}{sep}B1={s.Bank1:x4}{sep}B2={s.Bank2:x4}{sep}" +
                   $"B3={s.Bank3:x4}{sep}B4={s.Bank4:x4}{sep}B5={s.Bank5:x4}{sep}" +
                   $"B6={s.Bank6:x4}{sep}F0={s.Frame0:x4}{sep}F1={s.Frame1:x4}{sep}" +
                   $"F2={s.Frame2:x4}{sep}F3={s.Frame3:x4}{sep}F4={s.Frame4:x4}{sep}" +
                   $"F5={s.Frame5:x4}{sep}F6={s.Frame6:x4}{sep}F7={s.Frame7:x4}{sep}" +
                   $"F8={s.Frame8:x4}{sep}F9={s.Frame9:x4}{sep}F10={s.Frame10:x4}{sep}" +
                   $"F11={s.Frame11:x4}{sep}";
        }
    }
}