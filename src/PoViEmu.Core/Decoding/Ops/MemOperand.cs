using System;
using Iced.Intel;
using PoViEmu.Core.Hardware;
using PoViEmu.Core.Hardware.AckNow;

namespace PoViEmu.Core.Decoding.Ops
{
    public static class MemOpExt
    {
        public static ushort SegA(this MemOperand op, MachineState m)
        {
            var segA = m[op.Seg];
            return segA;
        }

        public static ushort OffA(this MemOperand op, MachineState m)
        {
            if (op.Off is { } offI)
            {
                return offI;
            }

            if (op.Idx is { } offR)
            {
                var offA = m[offR];
                return offA;
            }

            throw new InvalidOperationException($"{op} ?!");
        }
    }

    public record Mu16Operand(B16Register Seg, ushort? Off, B16Register? Idx)
        : MemOperand<ushort>(Seg, Off, Idx)
    {
        public override ushort this[MachineState m]
        {
            get => m.U16[this.SegA(m), this.OffA(m)];
            set => m.U16[this.SegA(m), this.OffA(m)] = value;
        }
    }

    public record Mu8Operand(B16Register Seg, ushort? Off, B16Register? Idx)
        : MemOperand<byte>(Seg, Off, Idx)
    {
        public override byte this[MachineState m]
        {
            get => m.U8[this.SegA(m), this.OffA(m)];
            set => m.U8[this.SegA(m), this.OffA(m)] = value;
        }
    }

    public abstract record MemOperand<T>(B16Register Seg, ushort? Off, B16Register? Idx)
        : MemOperand(Seg, Off, Idx)
    {
        public abstract T this[MachineState m] { get; set; }

        public override string ToString()
        {
            var off = Idx != null ? $"{Idx}" : $"{Off:X4}";
            var type = GetType().Name;
            return $"{type} [{Seg}:{off}]";
        }
    }

    public abstract record MemOperand(B16Register Seg, ushort? Off, B16Register? Idx)
        : BaseOperand;
}