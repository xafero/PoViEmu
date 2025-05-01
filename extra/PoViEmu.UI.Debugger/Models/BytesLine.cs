using PoViEmu.Base;

namespace PoViEmu.UI.Dbg.Models
{
    public record BytesLine(string Offset, byte[]? Bytes, string? Txt, string? Beta = null)
    {
        public string Hex => Beta ?? Bytes?.ToHex(prependSize: false, withSpace: true) ?? "?";

        public string Text => Txt ?? Bytes?.TryAsText() ?? "?";
    }
}