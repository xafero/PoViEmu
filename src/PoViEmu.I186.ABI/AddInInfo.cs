using System;
using TextHelper = PoViEmu.Base.DataHelper;

namespace PoViEmu.I186.ABI
{
    public sealed class AddInInfo
    {
        internal readonly AddInHeader _real;

        public AddInInfo(AddInHeader header)
            => _real = header;

        public string Signature
            => TextHelper.CleanUp(new string(_real.Signature));

        public AddInModel Model
            => TextHelper.ToEnum<AddInModel>(_real.Model, default);

        public Version HeaderVersion
            => TextHelper.ToVersion(_real.HeaderVersion);

        public AddInStatus Status
            => TextHelper.ToEnum<AddInStatus>(_real.Status, default);

        public AddInMode Mode
            => TextHelper.ToEnum<AddInMode>(_real.Mode, default);

        public string Name
            => TextHelper.CleanUp(new string(_real.Name));

        public uint Size
            => _real.Length;

        public DateTime Compiled
            => TextHelper.ToDate(_real.CompileDate, _real.CompileTime);

        public Version Version
            => TextHelper.ToVersion(_real.Version);

        public DateTime Library
            => TextHelper.ToDate(_real.LibraryDate, _real.LibraryTime);

        public Version LibraryVersion
            => TextHelper.ToVersion(_real.LibraryVersion);

        public uint OffsetIcon
            => _real.OffsetIcon;

        public uint OffsetLIcon
            => _real.OffsetLIcon;

        public string UserComment
            => TextHelper.CleanUp(_real.UserComment);
    }
}