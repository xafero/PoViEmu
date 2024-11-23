using PoViEmu.Core.Hardware;
using B16 = PoViEmu.Core.Hardware.AckNow.B16Register;

namespace PoViEmu.Core.Decoding.Ops.Mems
{
    public record Mu16Operand(B16 Seg, B16? Base, B16? Idx, short? Disp)
        : MemOperand<ushort>(Seg, Base, Idx, Disp)
    {
        public override ushort this[MachineState m]
        {
            get => m.U16[this.SegA(m), this.OffA(m)];
            set => m.U16[this.SegA(m), this.OffA(m)] = value;
        }
    }
}