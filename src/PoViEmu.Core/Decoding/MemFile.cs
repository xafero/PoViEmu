using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PoViEmu.Core.Decoding
{
    public sealed class MemFile : IMemBlob
    {
        public MemFile(string path, int offset = 0)
        {
            Path = path;
            Offset = offset;
        }

        public string Path { get; set; }
        public int Offset { get; set; }

        public IEnumerable<byte> GetBytes() => File.ReadAllBytes(Path).Skip(Offset);
    }
}