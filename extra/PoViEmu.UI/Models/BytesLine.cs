using PoViEmu.Base;
using PoViEmu.Base;
using PoViEmu.Common;

namespace PoViEmu.UI.Models
{
    public record BytesLine(string Offset, byte[] Bytes)
    {
        public string Hex => Bytes.ToHex(prependSize: false, withSpace: true);

        public string Txt => Bytes.TryAsText();
    }
}