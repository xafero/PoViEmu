using System;
using System.IO;
using System.Text;
using PoViEmu.Common;

namespace PoViEmu.Core.Dumps
{
    internal static class DumpDeep
    {
        private static readonly string YearT = new([(char)0x00, (char)0x00, (char)0x32, (char)0x30]);

        public static void LoadOsHeader(this DumpInfo header, Stream stream, int offset)
        {
            stream.Position = offset + 63400L;
            var array = new byte[2100];
            _ = stream.Read(array, 0, array.Length);

            var text = Encoding.ASCII.GetString(array);
            const string tmp = "OSHEADER";
            var parts = text.Split(tmp);

            var splat1I = parts[0].IndexOf(YearT, StringComparison.Ordinal);
            var splat1 = parts[0].Substring(splat1I, 18).FixSpaces();
            var splat2 = parts[1].Split(' ')[1][..17].FixSpaces();

            header.TimeStamp = TextHelper.ToDate(splat1[..8], splat1[8..12]);
            header.Version = TextHelper.ToVersion(splat1[12..16]);
            header.DeviceStamp = TextHelper.ToDate(splat2[..8], null);
            header.DeviceModel = TextHelper.ToEnum<DumpModel>(splat2[8..12], default);
        }
    }
}