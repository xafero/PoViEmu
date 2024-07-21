using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using PoViEmu.Core.Addins;
using PoViEmu.Common;
using static PoViEmu.Common.ErrorHelper;

namespace PoViEmu.Core.Dumps
{
    public static class DumpDeep
    {
        private static readonly string YearT = new([(char)0x00, (char)0x00, (char)0x32, (char)0x30]);

        internal static void LoadOsHeader(this DumpInfo header, Stream stream, int offset)
        {
            stream.Position = offset + 15;
            var array = new byte[1];
            _ = stream.Read(array, 0, array.Length);
            header.Flag = Encoding.ASCII.GetString(array)[0];

            stream.Position = offset + 63400L;
            array = new byte[2100];
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

        public static void LoadOsAddIns(this DumpInfo header, Stream stream)
        {
            header.AddIns = FindAddIns(stream);
        }

        private static IDictionary<FileAddress, AddInInfo> FindAddIns(this Stream stream, int? limit = null)
        {
            var addIns = new Dictionary<FileAddress, AddInInfo>();
            var size = limit ?? stream.Length;
            for (var i = 0; i < size; i += 1024 / 2)
            {
                var j = i;
                _ = Try(() =>
                {
                    var parsed = AddInReader.Read(stream, j);
                    var begin = j;
                    var end = (int)(j + parsed.Size);
                    addIns.Add(new(begin, end), parsed);
                    return true;
                }, out _);
            }
            return addIns;
        }
    }
}