using PoViEmu.Core.Hardware;
using PoViEmu.Core.Hardware.AckNow;

namespace PoViEmu.Core.Decoding.Ops
{
    public record Mi16Operand(B16Register Seg, short? Off, B16Register? Idx)
        : MemOperand<short>(Seg, Off, Idx)
    {
        public override short this[MachineState m]
        {
            get => (short)m.U16[this.SegA(m), this.OffA(m)];
            set => m.U16[this.SegA(m), this.OffA(m)] = (ushort)value;
        }
    }
}