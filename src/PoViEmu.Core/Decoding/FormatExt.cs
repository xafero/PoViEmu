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
    }
}