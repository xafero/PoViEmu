namespace PoViEmu.Core.Decoding.Ops.Consts
{
    public record U16Operand(ushort Val) : NumOperand<ushort>;

    public record U8Operand(byte Val) : NumOperand<byte>;
}