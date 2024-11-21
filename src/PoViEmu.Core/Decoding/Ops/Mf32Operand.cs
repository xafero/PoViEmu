using PoViEmu.Core.Hardware;
using PoViEmu.Core.Hardware.AckNow;

namespace PoViEmu.Core.Decoding.Ops
{
    public record Mf32Operand(
        B16Register Seg,
        B16Register? Base,
        B16Register? Idx,
        short? Off
    )
        : MemOperand<float>(Seg, Base, Idx, Off)
    {
        public override float this[MachineState m]
        {
            get => m.U16[this.SegA(m), this.OffA(m)];
            set => m.U16[this.SegA(m), this.OffA(m)] = (ushort)value;
        }
    }
}