using System;
using System.IO;
using PoViEmu.I186.ABI;

namespace PoViEmu.Inventory.Utils
{
    public sealed class AddInInfoPlus<T>
    {
        public AddInInfoPlus(AddInInfo info, byte[] bytes, Func<Stream, T> func)
        {
            Info = info;
            OffsetIcon = func(ImageTools.LoadImage(bytes, info.OffsetIcon));
            OffsetLIcon = func(ImageTools.LoadImage(bytes, info.OffsetLIcon));
        }

        public AddInInfo Info { get; }
        public T OffsetIcon { get; }
        public T OffsetLIcon { get; }
    }
}