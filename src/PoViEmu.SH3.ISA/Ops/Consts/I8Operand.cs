namespace PoViEmu.SH3.ISA.Ops.Consts
{
    public record I8Operand(sbyte Val) : NumOperand<sbyte>
    {
        public override string ToString()
        {
            var val = Val;
            return $"#{val}";
        }
    }
}