using B16 = PoViEmu.SH3.ISA.ShRegister;

namespace PoViEmu.SH3.ISA.Ops.Mems
{
    public record Mi16Operand(B16 Seg, B16? Base, B16? Idx, short? Disp)
        : MemOperand<short>(Seg, Base, Idx, Disp)
    {
        public override short this[IMachineState m]
        {
            get => (short)m.U16[this.OffA(m)];
            set => m.U16[this.OffA(m)] = (ushort)value;
        }

        public override string ToDebug(bool v) => ToString();
    }
}