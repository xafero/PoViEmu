namespace PoViEmu.SH3.ISA.Ops
{
    /// <summary>
    /// iiii: Immediate data
    /// </summary>
    public sealed record ImmedUOperand(byte Val) : BaseOperand
    {
        public override string ToDebug(bool v)
        {
            return $"iu{(v ? $"{{{Val}}}" : "")}";
        }

        public override string ToString()
        {
            return $"#{Val}";
        }
    }
}