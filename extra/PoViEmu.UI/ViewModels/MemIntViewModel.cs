using PoViEmu.Base;
using PoViEmu.UI.Models;

namespace PoViEmu.UI.ViewModels
{
    public class MemIntViewModel : MemViewModel
    {
        public void Read(ushort segment, ushort offset, byte[] bytes, int lineSize = 16)
        {
            Lines.Clear();
            foreach (var oneArray in bytes.SplitEvery(lineSize))
            {
                var txt = oneArray.DecodeChars();
                var hex = oneArray.ToHex(false, true);
                var off = $"{segment:X4}:{offset:X4}";
                Lines.Add(new BytesLine(off, hex, txt));
                offset = (ushort)(offset + oneArray.Length);
            }
        }
    }
}