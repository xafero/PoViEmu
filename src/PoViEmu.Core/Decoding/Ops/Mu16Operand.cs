using PoViEmu.Core.Hardware;
using PoViEmu.Core.Hardware.AckNow;

namespace PoViEmu.Core.Decoding.Ops
{
    public record Mu16Operand(B16Register Seg, ushort? Off, B16Register? Idx)
        : MemOperand<ushort>(Seg, Off, Idx)
    {
        public override ushort this[MachineState m]
        {
            get => m.U16[this.SegA(m), this.OffA(m)];
            set => m.U16[this.SegA(m), this.OffA(m)] = value;
        }
    }
}