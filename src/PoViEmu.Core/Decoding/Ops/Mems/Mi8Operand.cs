using PoViEmu.Core.Hardware;
using B16 = PoViEmu.Core.Hardware.AckNow.B16Register;

namespace PoViEmu.Core.Decoding.Ops.Mems
{
    public record Mi8Operand(B16 Seg, B16? Base, B16? Idx, short? Disp)
        : MemOperand<sbyte>(Seg, Base, Idx, Disp)
    {
        public override sbyte this[MachineState m]
        {
            get => (sbyte)m.U16[this.SegA(m), this.OffA(m)];
            set => m.U16[this.SegA(m), this.OffA(m)] = (ushort)value;
        }
    }
}