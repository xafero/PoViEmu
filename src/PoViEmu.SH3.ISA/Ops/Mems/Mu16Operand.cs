using B16 = PoViEmu.SH3.ISA.ShRegister;

namespace PoViEmu.SH3.ISA.Ops.Mems
{
    public record Mu16Operand(B16 Seg, B16? Base, B16? Idx, short? Disp)
        : MemOperand<ushort>(Seg, Base, Idx, Disp)
    {
        public override ushort this[IMachineState m]
        {
            get => m.U16[this.OffA(m)];
            set => m.U16[this.OffA(m)] = value;
        }

        public override string ToDebug(bool v) => ToString();
    }
}