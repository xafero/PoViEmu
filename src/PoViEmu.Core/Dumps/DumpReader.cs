using System;
using System.IO;
using PoViEmu.Common;

namespace PoViEmu.Core.Dumps
{
    public static class DumpReader
    {
        public const int HeaderSize = 144;

        public static DumpInfo Read(byte[] bytes)
        {
            var gotSize = bytes.Length;
            if (gotSize < HeaderSize)
                throw new InvalidOperationException($"Got {gotSize} bytes instead of {HeaderSize}!");
            var header = Marshalling.Read<DumpHeader>(bytes);
            var result = new DumpInfo(header);
            CheckIfValid(result);
            return result;
        }

        private static void CheckIfValid(DumpInfo result)
        {
            try
            {
                _ = result.Signature;
                _ = result.Model;
                if (!result.Signature.Contains("E CASIO"))
                    throw new IndexOutOfRangeException("Invalid signature!");
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException("Could not parse the header!");
            }
        }

        public static DumpInfo Read(Stream stream, int offset = 0)
        {
            var buffer = new byte[HeaderSize];
            stream.Position = offset;
            _ = stream.Read(buffer, 0, HeaderSize);
            var header = Read(buffer);
            header.LoadOsHeader(stream, offset);
            return header;
        }

        public static DumpHeader GetInternal(this DumpInfo info)
        {
            return info._real;
        }
    }
}