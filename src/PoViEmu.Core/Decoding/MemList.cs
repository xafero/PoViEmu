using System;
using System.Collections.Generic;

// ReSharper disable UnusedMember.Global

namespace PoViEmu.Core.Decoding
{
    public sealed class MemList : IMemBlob
    {
        public MemList(List<byte> list)
        {
            List = list;
        }

        public MemList(IMemBlob blob) : this(blob.GetBytes())
        {
        }

        public MemList(IEnumerable<byte> bytes) : this([..bytes])
        {
        }

        public MemList() : this(Array.Empty<byte>())
        {
        }

        public List<byte> List { get; set; }

        public IEnumerable<byte> GetBytes() => List;
    }
}