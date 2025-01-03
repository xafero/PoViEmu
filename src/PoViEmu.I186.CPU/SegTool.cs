using System;
using System.Collections.Generic;

namespace PoViEmu.I186.CPU
{
    public static class SegTool
    {
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
        
        public static uint ToPhysicalAddress(ushort segment, ushort offset)
            => (uint)(segment * 16) + offset;

        public static void ParseSrc(string? addr, out ushort seg, out ushort off)
        {
            var parts = addr?.Split(':', 2);
            if (parts?.Length != 2)
                throw new InvalidOperationException(addr);
            seg = Convert.ToUInt16(parts[0], 16);
            off = Convert.ToUInt16(parts[1], 16);
        }
        
        public static string GetSrc<T>(ushort seg, ushort off)
        {
            var name = typeof(T).Name;
            switch (name)
            {
                case "Byte": return $"U8|{seg:X4}:{off:X4}";
                case "Byte[]": return $"U8A|{seg:X4}:{off:X4}";
                case "UInt16": return $"U16|{seg:X4}:{off:X4}";
                case "UInt16[]": return $"U16A|{seg:X4}:{off:X4}";
            }
            throw new InvalidOperationException($"{name} {seg} {off}");
        }
    }
}