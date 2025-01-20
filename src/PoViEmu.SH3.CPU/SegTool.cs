using System;
using System.Collections.Generic;
using PoViEmu.Base;

namespace PoViEmu.SH3.CPU
{
    public static class SegTool
    {
        public static void Write(byte[] mem, uint offset, IEnumerable<byte> bytes)
        {
            var i = 0;
            foreach (var bits in bytes)
                mem.CheckSet(offset + i++, bits);
        }

        public static IEnumerable<byte> Read(byte[] mem, uint offset, int count)
        {
            for (var i = 0; i < count; i++)
                yield return mem.CheckGet(offset + i);
        }

        public static void ParseSrc(string? addr, out uint off)
        {
            off = Convert.ToUInt32(addr, 16);
        }

        public static string GetSrc<T>(uint off)
        {
            var name = typeof(T).Name;
            switch (name)
            {
                case "Byte": return $"U8|{off:X8}";
                case "Byte[]": return $"U8A|{off:X8}";
                case "UInt16": return $"U16|{off:X8}";
                case "UInt16[]": return $"U16A|{off:X8}";
                case "UInt32": return $"U32|{off:X8}";
                case "UInt32[]": return $"U32A|{off:X8}";
            }
            throw new InvalidOperationException($"{name} {off}");
        }
    }
}