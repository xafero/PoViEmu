using B16 = PoViEmu.I186.ISA.B16Register;

namespace PoViEmu.I186.ISA.Ops.Mems
{
    public record Mu16Operand(B16 Seg, B16? Base, B16? Idx, short? Disp)
        : MemOperand<ushort>(Seg, Base, Idx, Disp)
    {
        public override ushort this[IMachineState m]
        {
            get => m.U16[this.SegA(m), this.OffA(m)];
            set => m.U16[this.SegA(m), this.OffA(m)] = value;
        }
    }
}