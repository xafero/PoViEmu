using System;
using PoViEmu.Common;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using PoViEmu.Core.Decoding;

namespace PoViEmu.Core.Hardware
{
    public static class FormatTool
    {
        public static List<string> ToAllStrings(this MachineState s)
        {
            var bld = new List<string>();
            var nl = Environment.NewLine;
            bld.AddRange(s.ToRegisterString(nl).Split(nl));
            bld.Add(string.Empty);
            bld.AddRange(s.ToStackString(nl).Split(nl));
            bld.Add(string.Empty);
            bld.AddRange(s.ToCodeString(nl).Split(nl));
            bld.Add(string.Empty);
            bld.AddRange(s.ToMemoryString(nl).Split(nl));
            bld.Add(string.Empty);
            return bld;
        }

        public static string ToAllString(this MachineState s, string? nl = null)
        {
            var bld = new StringBuilder();
            nl ??= Environment.NewLine;
            bld.Append(s.ToRegisterString(nl));
            bld.AppendLine();
            bld.Append(s.ToStackString(nl));
            bld.AppendLine();
            bld.Append(s.ToCodeString(nl));
            bld.AppendLine();
            bld.Append(s.ToMemoryString(nl));
            bld.AppendLine();
            return bld.ToString();
        }

        public static IEnumerable<XInstruction> ToInstructions(this MachineState s,
            ushort? segment = null, ushort? offset = null, int count = 256)
        {
            var mSegment = segment ?? s.CS;
            var mOffset = offset ?? s.IP;
            var buffer = s.ReadMemory(mSegment, mOffset, count).ToArray();
            using var mem = new MemoryStream(buffer);
            using var reader = new MemCodeReader(mem);
            foreach (var item in reader.Decode(mOffset))
                yield return item;
        }

        public static string ToCodeString(this MachineState s, string? rawSep = null,
            ushort? segment = null, ushort offset = 0, int count = 16, ushort? ip = null)
        {
            var sep = rawSep ?? Environment.NewLine;
            var bld = new StringBuilder();
            var mSegment = segment ?? s.CS;
            foreach (var item in ToInstructions(s, mSegment, offset, count))
            {
                var l = item.Parsed.IP16 == (ip ?? s.IP) ? "*" : "";
                var text = item.ToString($"{mSegment:X4}:");
                if (l.Length >= 1)
                    text = text.ReplaceFirst(" ", l);
                bld.Append(text);
                bld.Append(sep);
            }
            return bld.ToString();
        }

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
                   $"B0={s.Bank0:x4}{sep}B1={s.Bank1:x4}{sep}B2={s.Bank2:x4}{sep}" +
                   $"B3={s.Bank3:x4}{sep}B4={s.Bank4:x4}{sep}B5={s.Bank5:x4}{sep}" +
                   $"B6={s.Bank6:x4}{sep}F0={s.Frame0:x4}{sep}F1={s.Frame1:x4}{sep}" +
                   $"F2={s.Frame2:x4}{sep}F3={s.Frame3:x4}{sep}F4={s.Frame4:x4}{sep}" +
                   $"F5={s.Frame5:x4}{sep}F6={s.Frame6:x4}{sep}F7={s.Frame7:x4}{sep}" +
                   $"F8={s.Frame8:x4}{sep}F9={s.Frame9:x4}{sep}F10={s.Frame10:x4}{sep}" +
                   $"F11={s.Frame11:x4}{sep}";
        }
        
        public static string ToStackString(this MachineState s, string? rawSep = null,
            ushort add = 0, bool rev = false, int count = 20, ushort? sp = null)
        {
            var sep = rawSep ?? Environment.NewLine;
            var bld = new StringBuilder();
            var vals = s.GetStackVals(count: count);
            if (rev) vals = vals.Reverse();
            foreach (var t in vals)
            {
                var l = t.addr == (sp ?? s.SP) ? "*" : " ";
                var text = $"SS:{add + t.addr:X4}{l}  {t.val:x4}";
                bld.Append(text);
                bld.Append(sep);
            }
            return bld.ToString();
        }

        public static IEnumerable<(ushort addr, ushort val)> GetStackVals(this MachineState m,
            ushort? segment = null, int count = MachTool.SegmentSize, ushort offset = 0)
            => m.ReadMemory(segment ?? m.SS, offset, count - offset).SplitTwo().Select((t, i) =>
                ((ushort)(offset + i * 2), BitConverter.ToUInt16([t.first, t.second])));

        public static string ToMemoryString(this MachineState s, string? rawSep = null,
            ushort? segment = null, ushort offset = 0, int count = 256)
        {
            var sep = rawSep ?? Environment.NewLine;
            var bld = new StringBuilder();
            const int step = 16;
            var i = 0;
            var mSegment = segment ?? s.CS;
            var bytes = s.ReadMemory(mSegment, offset, count);
            foreach (var line in bytes.SplitIt(step))
            {
                var rawByteStr = string.Join(" ", line.Select(b => $"{b:X2}"));
                var text =
                    $"{mSegment:X4}:{offset + i * step:X4}   {rawByteStr.AddSpaceTo(47)}   {line.DecodeChars()}";
                bld.Append(text);
                bld.Append(sep);
                i++;
            }
            return bld.ToString();
        }
    }
}