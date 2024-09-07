using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PoViEmu.Core.Decoding
{
    public sealed class MemFile : IMemBlob
    {
        public MemFile(string path, int offset = 0, int length = 0)
        {
            Path = path;
            Offset = offset;
            Length = length;
        }

        public string Path { get; set; }
        public int Offset { get; set; }
        public int Length { get; set; }

        public IEnumerable<byte> GetBytes()
        {
            var rawBytes = File.ReadAllBytes(Path);
            IEnumerable<byte> bytes = rawBytes;
            if (Offset >= 1)
                bytes = bytes.Skip(Offset);
            if (Length >= 1)
                bytes = bytes.Take(Length);
            return bytes;
        }
    }
}