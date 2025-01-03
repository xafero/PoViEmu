using B16 = PoViEmu.I186.ISA.B16Register;

namespace PoViEmu.I186.ISA.Ops.Mems
{
    public record Mi16Operand(B16 Seg, B16? Base, B16? Idx, short? Disp)
        : MemOperand<short>(Seg, Base, Idx, Disp)
    {
        public override short this[IMachineState m]
        {
            get => (short)m.U16[this.SegA(m), this.OffA(m)];
            set => m.U16[this.SegA(m), this.OffA(m)] = (ushort)value;
        }
    }
}