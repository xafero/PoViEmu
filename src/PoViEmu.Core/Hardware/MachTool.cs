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

        /*
        public static byte GetLow(ref ushort value)
            => (byte)(value & 0xFF);

        public static byte GetHigh(ref ushort value)
            => (byte)((value >> 8) & 0xFF);

        public static void SetLow(ref ushort value, byte low)
            => value = (ushort)((value & 0xFF00) | low);

        public static void SetHigh(ref ushort value, byte high)
            => value = (ushort)((value & 0x00FF) | (high << 8));
        */

        public static byte GetLow(ushort value)
            => (byte)(value & 0xFF);

        public static byte GetHigh(ushort value)
            => (byte)((value >> 8) & 0xFF);

        public static ushort SetLow(ushort value, byte low)
            => (ushort)((value & 0xFF00) | low);

        public static ushort SetHigh(ushort value, byte high)
            => (ushort)((value & 0x00FF) | (high << 8));

        public static bool Check(this Flagged flag, ref Flagged value)
        {
            return (value & flag) == flag;
        }

        public static void Apply(this Flagged flag, ref Flagged value, bool on)
        {
            if (on)
                value |= flag;
            else
                value &= ~flag;
        }

        public static Flagged Add(this Flagged flag, Flagged value, bool on)
        {
            return on ? value | flag : value & ~flag;
        }

        public static void SetFlags(this MachineState m, Flagged value)
        {
            m.CF = value.HasFlag(Flagged.Carry);
            m.PF = value.HasFlag(Flagged.Parity);
            m.AF = value.HasFlag(Flagged.Auxiliary);
            m.ZF = value.HasFlag(Flagged.Zero);
            m.SF = value.HasFlag(Flagged.Sign);
            m.TF = value.HasFlag(Flagged.Trap);
            m.IF = value.HasFlag(Flagged.Interrupt);
            m.DF = value.HasFlag(Flagged.Direction);
            m.OF = value.HasFlag(Flagged.Overflow);
        }

        private static bool GetBit(this ref ushort flag, Flagged value)
        {
            return (flag & (ushort)value) == (ushort)value;
        }

        private static void SetBit(this ref ushort flag, Flagged value, bool on)
        {
            if (on)
                flag = (ushort)(flag | (ushort)value);
            else
                flag = (ushort)(flag & ~(ushort)value);
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

        public static void PushAll(this MachineState m)
        {
            var tmp = m.SP;
            m.Push(m.AX);
            m.Push(m.CX);
            m.Push(m.DX);
            m.Push(m.BX);
            m.Push(tmp);
            m.Push(m.BP);
            m.Push(m.SI);
            m.Push(m.DI);
        }

        public static void IncOrDec(this MachineState m, byte val, bool useSi, bool useDi)
        {
            if (m.DF == false)
            {
                if (useSi) m.SI += val;
                if (useDi) m.DI += val;
            }
            else
            {
                if (useSi) m.SI -= val;
                if (useDi) m.DI -= val;
            }
        }

        public static (ushort low, ushort high) SplitInt(this int num)
        {
            var low = (ushort)(num & 0xFFFF);
            var high = (ushort)(num >> 16);
            return (low, high);
        }

        public static int CombineInt(this (ushort low, ushort high) t)
        {
            return (t.high << 16) | t.low;
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

        public static void ParseSrc(string? addr, out ushort seg, out ushort off)
        {
            var parts = addr?.Split(':', 2);
            if (parts?.Length != 2)
                throw new InvalidOperationException(addr);
            seg = Convert.ToUInt16(parts[0], 16);
            off = Convert.ToUInt16(parts[1], 16);
        }
    }
}