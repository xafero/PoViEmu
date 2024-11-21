using PoViEmu.Core.Hardware;
using PoViEmu.Core.Hardware.AckNow;

namespace PoViEmu.Core.Decoding.Ops
{
    public record Mi16Operand(
        B16Register Seg,
        B16Register? Base,
        B16Register? Idx,
        short? Off
    )
        : MemOperand<short>(Seg, Base, Idx, Off)
    {
        public override short this[MachineState m]
        {
            get => (short)m.U16[this.SegA(m), this.OffA(m)];
            set => m.U16[this.SegA(m), this.OffA(m)] = (ushort)value;
        }
    }
}