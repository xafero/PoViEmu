using PoViEmu.Base;
using PoViEmu.UI.Models;

namespace PoViEmu.UI.ViewModels
{
    public class MemHitViewModel : MemViewModel
    {
        public void Read(uint offset, byte[] bytes, int lineSize = 16)
        {
            Lines.Clear();
            foreach (var oneArray in bytes.SplitEvery(lineSize))
            {
                var txt = oneArray.DecodeChars();
                var hex = oneArray.ToHex(false, true);
                var off = $"{offset:X8}";
                Lines.Add(new BytesLine(off, hex, txt));
                offset = (ushort)(offset + oneArray.Length);
            }
        }
    }
}