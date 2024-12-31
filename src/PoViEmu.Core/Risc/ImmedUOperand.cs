namespace PoViEmu.Core.Risc
{
    /// <summary>
    /// iiii: Immediate data
    /// </summary>
    public sealed record ImmedUOperand(byte Val) : BaseOperand
    {
        public override string ToString()
        {
            return $"#{Val}";
        }
    }
}