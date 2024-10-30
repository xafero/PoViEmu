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

        public static ushort OffA(this MemOperand op)
        {
            var offA = op.Off;
            return offA;
        }
    }

    public record Mu16Operand(B16Register Seg, ushort Off) : MemOperand<ushort>(Seg, Off)
    {
        public override ushort this[MachineState m]
        {
            get => m.U16[this.SegA(m), this.OffA()];
            set => m.U16[this.SegA(m), this.OffA()] = value;
        }

        public override string ToString()
        {
            return $"{nameof(Mu16Operand)} [{Seg}:{Off:X4}]";
        }
    }

    public abstract record MemOperand<T>(B16Register Seg, ushort Off) : MemOperand(Seg, Off)
    {
        public abstract T this[MachineState m] { get; set; }
    }

    public abstract record MemOperand(B16Register Seg, ushort Off) : BaseOperand;
}