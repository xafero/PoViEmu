using B16 = PoViEmu.I186.ISA.B16Register;

namespace PoViEmu.I186.ISA.Ops.Mems
{
    public record Mf32Operand(B16 Seg, B16? Base, B16? Idx, short? Disp)
        : MemOperand<float>(Seg, Base, Idx, Disp)
    {
        public override float this[IMachineState m]
        {
            get => m.U16[this.SegA(m), this.OffA(m)];
            set => m.U16[this.SegA(m), this.OffA(m)] = (ushort)value;
        }
    }
}