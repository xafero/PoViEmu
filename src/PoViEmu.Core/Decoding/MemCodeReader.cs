using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PoViEmu.Core.Decoding
{
    public sealed class MemCodeReader : TrackCodeReader, IDisposable, IAsyncDisposable
    {
        public MemCodeReader(Stream stream, bool track = true)
        {
            Stream = stream;
            Position = 0;
            Offset = 0;
            Current = [];
            Track = track;
        }

        private Stream Stream { get; }
        public int Position { get; private set; }
        public int Offset { get; private set; }
        private List<int> Current { get; }
        private bool Track { get; }

        public override int ReadByte()
        {
            var read = Stream.ReadByte();
            if (read == -1)
                return read;
            if (Track)
                Current.Add(read);
            Position++;
            return read;
        }

        public override (int off, byte[] bit) GetReadBytes()
        {
            var last = Offset;
            var array = Current.Select(c => (byte)c).ToArray();
            Current.Clear();
            Offset = Position;
            return (last, array);
        }

        public void Dispose()
        {
            Current.Clear();
            Stream.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await Stream.DisposeAsync();
        }
    }
}