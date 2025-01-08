using B16 = PoViEmu.SH3.ISA.ShRegister;

namespace PoViEmu.SH3.ISA.Ops.Mems
{
    public record Mf32Operand(B16 Seg, B16? Base, B16? Idx, short? Disp)
        : MemOperand<float>(Seg, Base, Idx, Disp)
    {
        public override float this[IMachineState m]
        {
            get => m.U16[this.OffA(m)];
            set => m.U16[this.OffA(m)] = (ushort)value;
        }

        public override string ToDebug(bool v) => ToString();
    }
}