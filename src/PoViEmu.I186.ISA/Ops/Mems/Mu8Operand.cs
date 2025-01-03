using B16 = PoViEmu.I186.ISA.B16Register;

namespace PoViEmu.I186.ISA.Ops.Mems
{
    public record Mu8Operand(B16 Seg, B16? Base, B16? Idx, short? Disp)
        : MemOperand<byte>(Seg, Base, Idx, Disp)
    {
        public override byte this[IMachineState m]
        {
            get => m.U8[this.SegA(m), this.OffA(m)];
            set => m.U8[this.SegA(m), this.OffA(m)] = value;
        }
    }
}