using PoViEmu.Base;

namespace PoViEmu.UI.Dbg.Models
{
    public record BytesLine(string Offset, byte[] Bytes, string? Txt)
    {
        public string Hex => Bytes.ToHex(prependSize: false, withSpace: true);

        public string Text => Txt ?? Bytes.TryAsText();
    }
}