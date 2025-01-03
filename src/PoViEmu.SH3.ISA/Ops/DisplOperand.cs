namespace PoViEmu.SH3.ISA.Ops
{
    /// <summary>
    /// dddd: Displacement
    /// </summary>
    public sealed record DisplOperand(uint Val) : BaseOperand
    {
        public override string ToString()
        {
            return $"0x{Val:x}";
        }
    }
}