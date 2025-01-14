namespace PoViEmu.SH3.ISA.Ops.Jumps
{
    public record FarOperand(uint Off)
        : JumpOperand
    {
        public override string ToString()
        {
            return $"0x{Off:x8}";
        }
    }
}