using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.UI.Models;
using PoViEmu.Base;

namespace PoViEmu.UI.ViewModels
{
    public partial class RawMemViewModel : ViewModelBase
    {
        [ObservableProperty] private ObservableCollection<BytesLine> _lines = new();

        public void Read(ushort offset, byte[] bytes, int lineSize = 16)
        {
            Lines.Clear();
            foreach (var oneArray in bytes.SplitEvery(lineSize))
            {
                var txt = oneArray.DecodeChars();
                var hex = oneArray.ToHex(false, true);
                var off = $"{offset:X4}";
                Lines.Add(new BytesLine(off, hex, txt));
                offset = (ushort)(offset + oneArray.Length);
            }
        }
    }
}