namespace PoViEmu.SH3.ISA.Ops.Mems
{
    public record Mu16Operand(AddressingMode Mode, ShRegister Base, ShRegister? Idx, int? Disp)
        : MemOperand<ushort>(Mode, Base, Idx, Disp)
    {
        public override ushort this[IMachineState m]
        {
            get => m.U16[this.OffA(m)];
            set => m.U16[this.OffA(m)] = value;
        }

        public override byte ByteSize => 2;
    }
}