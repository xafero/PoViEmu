namespace PoViEmu.Core.Decoding.Ops.Jumps
{
    public record FarOperand(ushort Sel, ushort Dst) : JumpOperand
    {
    }
}