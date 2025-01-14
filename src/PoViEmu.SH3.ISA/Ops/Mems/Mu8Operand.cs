using B32 = PoViEmu.SH3.ISA.ShRegister;

namespace PoViEmu.SH3.ISA.Ops.Mems
{
    public record Mu8Operand(AddressingMode Mode, B32 Base, B32? Idx, int? Disp)
        : MemOperand<byte>(Mode, Base, Idx, Disp)
    {
        public override byte this[IMachineState m]
        {
            get => m.U8[this.OffA(m)];
            set => m.U8[this.OffA(m)] = value;
        }

        public override byte ByteSize => 1;
    }
}