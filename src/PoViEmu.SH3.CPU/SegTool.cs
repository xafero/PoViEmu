using System;
using System.Collections.Generic;

namespace PoViEmu.SH3.CPU
{
    public static class SegTool
    {
        public static void Write(byte[] mem, uint offset, IEnumerable<byte> bytes)
        {
            var physicalAddr = (offset);
            var i = 0;
            foreach (var bits in bytes)
                mem[physicalAddr + i++] = bits;
        }

        public static IEnumerable<byte> Read(byte[] mem, uint offset, int count)
        {
            var physicalAddr = (offset);
            for (var i = 0; i < count; i++)
                yield return mem[physicalAddr + i];
        }

        public static void ParseSrc(string? addr, out uint off)
        {
            var parts = addr?.Split(':', 2);
            if (parts?.Length != 2)
                throw new InvalidOperationException(addr);
            off = Convert.ToUInt32(parts[1], 16);
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
            }
            throw new InvalidOperationException($"{name} {off}");
        }
    }
}