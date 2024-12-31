namespace PoViEmu.Core.Risc
{
    /// <summary>
    /// mmmm: Source register
    /// </summary>
    public sealed record SourceReg(ShRegister Reg) : RegOperand;
}