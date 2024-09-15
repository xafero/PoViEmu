using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PoViEmu.Common;
using System.IO;
using PoViEmu.Core.Decoding;

namespace Discover
{
    public static class MachTool
    {
        public static byte GetLow(ref ushort value)
            => (byte)(value & 0xFF);

        public static byte GetHigh(ref ushort value)
            => (byte)((value >> 8) & 0xFF);

        public static void SetLow(ref ushort value, byte low)
            => value = (ushort)((value & 0xFF00) | low);

        public static void SetHigh(ref ushort value, byte high)
            => value = (ushort)((value & 0x00FF) | (high << 8));

        public static uint ToPhysicalAddress(ushort segment, ushort offset)
            => (uint)(segment * 16) + offset;

        public static (ushort s, ushort o) ToLogicalAddress(uint physicalAddress, ushort segment)
            => (segment, (ushort)(physicalAddress - segment * 16));

        public static byte[] AllocateMemory(double megaBytes = 1, byte defaultVal = 0xFF)
        {
            var byteSize = (int)(megaBytes * 1024 * 1024);
            var array = new byte[byteSize];
            Array.Fill(array, defaultVal);
            return array;
        }

        public static void Write(byte[] mem, ushort segment, ushort offset, IEnumerable<byte> bytes)
        {
            var physicalAddr = ToPhysicalAddress(segment, offset);
            var i = 0;
            foreach (var bits in bytes)
                mem[physicalAddr + i++] = bits;
        }

        public static IEnumerable<byte> Read(byte[] mem, ushort segment, ushort offset, int count)
        {
            var physicalAddr = ToPhysicalAddress(segment, offset);
            for (var i = 0; i < count; i++)
                yield return mem[physicalAddr + i];
        }

        public static void Push(this Machine m, ushort value)
        {
            m.SP -= 2;
            var bytes = BitConverter.GetBytes(value);
            m.WriteMemory(m.SS, m.SP, bytes);
        }

        public static ushort Pop(this Machine m)
        {
            var bytes = m.ReadMemory(m.SS, m.SP, 2).ToArray();
            var value = BitConverter.ToUInt16(bytes, 0);
            m.SP += 2;
            return value;
        }

        public static string ToStackString(this Machine s, string? rawSep = null,
            ushort add = 0, bool rev = false, int count = 20, ushort? sp = null)
        {
            var sep = rawSep ?? Environment.NewLine;
            var bld = new StringBuilder();
            var vals = s.GetStackVals(count: count);
            if (rev) vals = vals.Reverse();
            foreach (var t in vals)
            {
                var l = t.addr == (sp ?? s.SP) ? "*" : " ";
                var text = $"SS:{(add + t.addr):X4}{l}  {t.val:x4}";
                bld.Append(text);
                bld.Append(sep);
            }
            return bld.ToString();
        }

        public static IEnumerable<(ushort addr, ushort val)> GetStackVals(this Machine m,
            ushort? segment = null, int count = 64 * 1024, ushort offset = 0)
            => m.ReadMemory(segment ?? m.SS, offset, count - offset).SplitTwo().Select((t, i) =>
                ((ushort)(offset + i * 2), BitConverter.ToUInt16([t.first, t.second])));
        
        public static IEnumerable<XInstruction> ToInstructions(this Machine s,
            ushort? segment = null, ushort offset = 0, int count = 256)
        {
            var mSegment = segment ?? s.CS;
            var buffer = s.ReadMemory(mSegment, offset, count).ToArray();
            using var mem = new MemoryStream(buffer);
            using var reader = new MemCodeReader(mem);
            foreach (var item in reader.Decode(offset))
                yield return item;
        }
        
        public static string ToCodeString(this Machine s, string? rawSep = null,
            ushort? segment = null, ushort offset = 0, int count = 256, ushort? ip = null)
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
        
        public static string ToMemoryString(this Machine s, string? rawSep = null,
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
                var text = $"{mSegment:X4}:{offset + (i * step):X4}   " +
                           $"{rawByteStr.AddSpaceTo(47)}   " +
                           $"{line.DecodeChars()}";
                bld.Append(text);
                bld.Append(sep);
                i++;
            }
            return bld.ToString();
        }
    }
}