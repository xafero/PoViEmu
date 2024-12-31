namespace PoViEmu.Core.Risc
{
    /// <summary>
    /// dddd: Displacement
    /// </summary>
    public sealed record DisplOperand(ushort Val) : BaseOperand
    {
        public override string ToString()
        {
            return $"0x{Val:x}";
        }
    }
}