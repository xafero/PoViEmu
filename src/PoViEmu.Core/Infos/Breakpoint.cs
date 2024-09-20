using System;

namespace PoViEmu.Core.Infos
{
    public record Breakpoint(string Segment, string Offset, string Physical) : IComparable
    {
        public int CompareTo(object obj)
            => string.Compare(ToString(), obj?.ToString(), StringComparison.Ordinal);
    }
}