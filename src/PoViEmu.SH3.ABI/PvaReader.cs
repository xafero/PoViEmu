using System;
using System.IO;
using PoViEmu.Base;

namespace PoViEmu.SH3.ABI
{
    public static class PvaReader
    {
        public const int HeaderSize = 416;

        public static PvaInfo Read(byte[] bytes)
        {
            var gotSize = bytes.Length;
            if (gotSize < HeaderSize)
                throw new InvalidOperationException($"Got {gotSize} bytes instead of {HeaderSize}!");
            var header = Marshalling.Read<PvaHeader>(bytes);
            var result = new PvaInfo(header, (uint)bytes.Length);
            CheckIfValid(result);
            return result;
        }

        private static void CheckIfValid(PvaInfo result)
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

        public static PvaInfo Read(Stream stream, int offset = 0)
        {
            var buffer = new byte[HeaderSize];
            stream.Position = offset;
            _ = stream.Read(buffer, 0, HeaderSize);
            var header = Read(buffer);
            return header;
        }

        public static PvaHeader GetInternal(this PvaInfo info)
        {
            return info._real;
        }
    }
}