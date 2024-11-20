using PoViEmu.Core.Hardware;
using PoViEmu.Core.Hardware.AckNow;

namespace PoViEmu.Core.Decoding.Ops
{
    public record Mu8Operand(B16Register Seg, short? Off, B16Register? Idx)
        : MemOperand<byte>(Seg, Off, Idx)
    {
        public override byte this[MachineState m]
        {
            get => m.U8[this.SegA(m), this.OffA(m)];
            set => m.U8[this.SegA(m), this.OffA(m)] = value;
        }
    }
}