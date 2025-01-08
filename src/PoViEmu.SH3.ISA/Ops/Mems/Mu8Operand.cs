using B16 = PoViEmu.SH3.ISA.ShRegister;

namespace PoViEmu.SH3.ISA.Ops.Mems
{
    public record Mu8Operand(B16 Seg, B16? Base, B16? Idx, short? Disp)
        : MemOperand<byte>(Seg, Base, Idx, Disp)
    {
        public override byte this[IMachineState m]
        {
            get => m.U8[this.OffA(m)];
            set => m.U8[this.OffA(m)] = value;
        }

        public override string ToDebug(bool v) => ToString();
    }
}