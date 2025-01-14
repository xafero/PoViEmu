namespace PoViEmu.SH3.ISA.Ops.Mems
{
    public record Mu32Operand(AddressingMode Mode, ShRegister Base, ShRegister? Idx, int? Disp)
        : MemOperand<uint>(Mode, Base, Idx, Disp)
    {
        public override uint this[IMachineState m]
        {
            get => m.U32[this.OffA(m)];
            set => m.U32[this.OffA(m)] = value;
        }

        public override byte ByteSize => 4;
    }
}