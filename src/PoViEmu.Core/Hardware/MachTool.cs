using System;
using System.Collections.Generic;
using System.Linq;

namespace PoViEmu.Core.Hardware
{
    public static class MachTool
    {
        public const int SegmentSize = 64 * 1024;

        public static byte[] AllocateMemory(double megaBytes = 1, byte defaultVal = 0xFF)
        {
            var byteSize = (int)(megaBytes * 1024 * 1024);
            var array = new byte[byteSize];
            Array.Fill(array, defaultVal);
            return array;
        }

        public static uint ToPhysicalAddress(ushort segment, ushort offset)
            => (uint)(segment * 16) + offset;

        public static (ushort seg, ushort off) ToLogicalAddress(uint physical, ushort segment)
            => (segment, (ushort)(physical - segment * 16));

        public static byte GetLow(ref ushort value)
            => (byte)(value & 0xFF);

        public static byte GetHigh(ref ushort value)
            => (byte)((value >> 8) & 0xFF);

        public static void SetLow(ref ushort value, byte low)
            => value = (ushort)((value & 0xFF00) | low);

        public static void SetHigh(ref ushort value, byte high)
            => value = (ushort)((value & 0x00FF) | (high << 8));

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

        public static void Push(this MachineState m, ushort value)
        {
            m.SP -= 2;
            var bytes = BitConverter.GetBytes(value);
            m.WriteMemory(m.SS, m.SP, bytes);
        }

        public static ushort Pop(this MachineState m)
        {
            var bytes = m.ReadMemory(m.SS, m.SP, 2).ToArray();
            var value = BitConverter.ToUInt16(bytes, 0);
            m.SP += 2;
            return value;
        }
    }
}