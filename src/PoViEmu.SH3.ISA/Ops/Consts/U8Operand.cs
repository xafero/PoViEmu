namespace PoViEmu.SH3.ISA.Ops.Consts
{
    public record U8Operand(byte Val) : NumOperand<byte>
    {
        public override string ToString()
        {
            var val = Val;
            return $"#{val}";
        }
    }
}