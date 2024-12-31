namespace PoViEmu.Core.Risc
{
    public sealed record MemoryOperand(
        ShRegister Off, ShRegister? Base = null,
        bool IsPlus = false, bool IsMinus = false) 
        : BaseOperand
    {
        public override string ToString()
        {
            var @base = Base?.Name();
            var off = Off.Name();
            var suffix = IsPlus ? "+" : "";
            var prefix = IsMinus ? "-" : "";
            if (@base == null)
                return $"@{prefix}{off}{suffix}";

            return $"@({off},{@base})";
        }
    }
}