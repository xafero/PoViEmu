using PoViEmu.SH3.ISA.Ops.Places;
using B32 = PoViEmu.SH3.ISA.ShRegister;

namespace PoViEmu.SH3.ISA.Ops.Mems
{
    public record Mu32Operand(AddressingMode Mode, B32 Base, B32? Idx, int? Disp, bool Align = false)
        : MemOperand<uint>(Mode, Base, Idx, Disp, Align)
    {
        public override uint this[IMachineState m]
        {
            get => m.U32[this.OffA(m)];
            set => m.U32[this.OffA(m)] = value;
        }

        public override byte ByteSize => 4;
    }
}