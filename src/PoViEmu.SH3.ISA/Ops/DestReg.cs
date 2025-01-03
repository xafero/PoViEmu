namespace PoViEmu.SH3.ISA.Ops
{
    /// <summary>
    /// nnnn: Destination register
    /// </summary>
    public sealed record DestReg(ShRegister Reg) : RegOperand;
}