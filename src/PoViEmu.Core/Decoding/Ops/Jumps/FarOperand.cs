namespace PoViEmu.Core.Decoding.Ops.Jumps
{
    public record FarOperand(ushort Seg, ushort Off)
        : JumpOperand
    {
    }
}