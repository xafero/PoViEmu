using System;
using System.IO;
using PoViEmu.Core.Addins;

namespace PoViEmu.Core.Images
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