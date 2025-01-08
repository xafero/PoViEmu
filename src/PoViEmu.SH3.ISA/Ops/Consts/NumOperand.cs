namespace PoViEmu.SH3.ISA.Ops.Consts
{
    public abstract record NumOperand<T> : BaseOperand
    {
        public abstract T Val { get; init; }
    }

    public record I8Operand(sbyte Val) : NumOperand<sbyte>
    {
    }

    public record I16Operand(short Val) : NumOperand<short>
    {
    }

    public record I32Operand(int Val) : NumOperand<int>
    {
    }

    public record U8Operand(byte Val) : NumOperand<byte>
    {
    }

    public record U16Operand(ushort Val) : NumOperand<ushort>
    {
    }

    public record U32Operand(uint Val) : NumOperand<uint>
    {
    }
}