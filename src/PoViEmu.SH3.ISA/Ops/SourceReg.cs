namespace PoViEmu.SH3.ISA.Ops
{
    /// <summary>
    /// mmmm: Source register
    /// </summary>
    public sealed record SourceReg(ShRegister Reg) : RegOperand;
}