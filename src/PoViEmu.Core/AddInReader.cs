using System;
using System.IO;
using PoViEmu.Common;
using PoViEmu.Core.Addins;

namespace PoViEmu.Core
{
    public static class AddInReader
    {
        public const int HeaderSize = 144;

        public static AddInInfo Read(byte[] bytes)
        {
            var gotSize = bytes.Length;
            if (gotSize < HeaderSize)
                throw new InvalidOperationException($"Got {gotSize} bytes instead of {HeaderSize}!");
            var header = Marshalling.Read<AddInHeader>(bytes);
            var result = new AddInInfo(header);
            CheckIfValid(result);
            return result;
        }

        private static void CheckIfValid(AddInInfo result)
        {
            try
            {
                _ = result.HeaderVersion;
                _ = result.LibraryVersion;
                _ = result.Version;
            }
            catch (Exception ex) when (ex is IndexOutOfRangeException or FormatException)
            {
                throw new InvalidOperationException("Could not parse the header!");
            }
        }

        public static AddInInfo Read(Stream stream, int offset = 0)
        {
            var buffer = new byte[HeaderSize];
            stream.Position = offset;
            _ = stream.Read(buffer, 0, HeaderSize);
            var header = Read(buffer);
            return header;
        }

        public static AddInHeader GetInternal(this AddInInfo info)
        {
            return info._real;
        }
    }
}