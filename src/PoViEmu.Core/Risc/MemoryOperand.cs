namespace PoViEmu.Core.Risc
{
    public sealed record MemoryOperand(ShRegister Off, ShRegister? Base = null, bool isPlus = false)
        : BaseOperand
    {
        public override string ToString()
        {
            var @base = Base?.Name();
            var off = Off.Name();
            var suffix = isPlus ? "+" : "";
            if (@base == null)
                return $"@{off}{suffix}";

            return $"@({off},{@base})";
        }
    }
}