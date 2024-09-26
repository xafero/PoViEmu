using System;
using System.IO;
using System.Linq;
using System.Text;
using PoViEmu.Common;
using static PoViEmu.Core.Data.TwfDecoder;

namespace PoViEmu.Core.Data
{
    public static class TwfReader
    {
        public static TwfInfo Read(Stream stream)
        {
            byte[] bytes;
            using (stream)
            {
                var buffer = new byte[64 * 1024];
                var got = stream.Read(buffer, 0, buffer.Length);
                bytes = buffer[..got];
            }
            return Read(bytes);
        }

        public static TwfInfo Read(byte[] bytes)
        {
            Decode(bytes, bytes.Length);

            var head = $"%{ToText(bytes[11..59], "_").Trim('_').Trim('(').Split('%', 3).Last()}";
            var modelTxt = ToText(bytes[510..527], "_").Trim('(', '$', '_');
            var model = Enum.Parse<TwfModel>(modelTxt.Replace('-', '_'), false);

            var info = new TwfInfo
            {
                AppInfo = head,
                Device = model,
                Size = bytes.Length
            };

            string[] headers = [$"{(char)0}CYCMemoItem{(char)0}"];

            foreach (var header in headers)
            {
                var hBytes = Encoding.ASCII.GetBytes(header);
                var pos = bytes.FindArray(hBytes);
                if (pos < 0)
                    continue;
                var hTxt = ToText(bytes[pos..(pos + 220)], "_");
                var hPts = hTxt.Split("_!", 2);
                if (hPts.Length != 2)
                    continue;
                var hName = hPts[1].Split('_', 2)[0].Trim('*', '+', ' ');
                info.ProgName = hName;
            }

            return info;
        }
    }
}