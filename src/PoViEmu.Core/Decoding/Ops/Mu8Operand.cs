using PoViEmu.Core.Hardware;
using PoViEmu.Core.Hardware.AckNow;

namespace PoViEmu.Core.Decoding.Ops
{
    public record Mu8Operand(
        B16Register Seg,
        B16Register? Base,
        B16Register? Idx,
        short? Off
    )
        : MemOperand<byte>(Seg, Base, Idx, Off)
    {
        public override byte this[MachineState m]
        {
            get => m.U8[this.SegA(m), this.OffA(m)];
            set => m.U8[this.SegA(m), this.OffA(m)] = value;
        }
    }
}