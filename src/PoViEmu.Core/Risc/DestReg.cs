namespace PoViEmu.Core.Risc
{
    /// <summary>
    /// nnnn: Destination register
    /// </summary>
    public sealed record DestReg(ShRegister Reg) : RegOperand;
}