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
    }

    public record Mu16Operand(AddressingMode Mode, B32 Base, B32? Idx, int? Disp)
        : MemOperand<ushort>(Mode, Base, Idx, Disp)
    {
        public override ushort this[IMachineState m]
        {
            get => m.U16[this.OffA(m)];
            set => m.U16[this.OffA(m)] = value;
        }
    }

    public record Mu32Operand(AddressingMode Mode, B32 Base, B32? Idx, int? Disp)
        : MemOperand<uint>(Mode, Base, Idx, Disp)
    {
        public override uint this[IMachineState m]
        {
            get => m.U32[this.OffA(m)];
            set => m.U32[this.OffA(m)] = value;
        }
    }
}