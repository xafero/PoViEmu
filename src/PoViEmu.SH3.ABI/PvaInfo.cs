using System;
using System.Drawing;
using TextHelper = PoViEmu.Base.DataHelper;

namespace PoViEmu.SH3.ABI
{
    public sealed class PvaInfo
    {
        internal readonly PvaHeader _real;

        public PvaInfo(PvaHeader header, uint length)
        {
            _real = header;
            Size = length;
        }

        public string Signature
            => TextHelper.CleanUp(new string(_real.Signature));

        public PvaModel Model
            => Signature == "PVAPLHEDV20" ? PvaModel.H16 : default;

        public Version HeaderVersion
            => TextHelper.ToHVersion(_real.HeaderVersion);

        public PvaType Type
            => TextHelper.ToEnum<PvaType>(_real.PvaType, default);

        public PvaMode Mode
            => TextHelper.ToEnum<PvaMode>(_real.PvaMode, default);

        public string Name
            => TextHelper.CleanUp(new string(_real.ProgramName));

        public uint Size { get; }

        public DateTime Compiled
        {
            get
            {
                var date = $"{_real.CreationYear[0]:x2}{_real.CreationYear[1]:x2}" +
                           $"{_real.CreationMonth:x2}{_real.CreationDay:x2}";
                var time = $"{_real.CreationDay:x2}{_real.CreationHour:x2}";
                return TextHelper.ToDate(date, time);
            }
        }

        public Version Version
            => TextHelper.ToHVersion(_real.ProgramVersion);

        public Version LibraryVersion
            => TextHelper.ToHVersion(_real.LibraryVersion);

        public Size SizeIcon
            => new(_real.IconWidth[1], _real.IconHeight[1]);

        public Size SizeLIcon
            => new(_real.SmallIconWidth[1], _real.SmallIconHeight[1]);
    }
}