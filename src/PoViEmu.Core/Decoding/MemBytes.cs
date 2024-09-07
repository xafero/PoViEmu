using System.Collections.Generic;
using System.Linq;

// ReSharper disable UnusedMember.Global

namespace PoViEmu.Core.Decoding
{
    public sealed class MemList : IMemBlob
    {
        public MemList(List<byte> list)
        {
            List = list;
        }

        public MemList(IMemBlob blob) : this(blob.GetBytes().ToList())
        {
        }

        public MemList() : this(new List<byte>())
        {
        }

        public List<byte> List { get; set; }

        public IEnumerable<byte> GetBytes() => List;
    }
}