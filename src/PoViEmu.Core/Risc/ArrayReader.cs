using System.IO;

namespace PoViEmu.Core.Risc
{
    public sealed class ArrayReader : IReader
    {
        private readonly byte[] _bytes;
        private int _index;

        public ArrayReader(byte[] bytes)
        {
            _bytes = bytes;
            _index = -1;
        }

        public byte ReadNextByte()
        {
            _index++;
            if (_index >= _bytes.Length)
                throw new EndOfStreamException();
            var raw = _bytes[_index];
            return raw;
        }
    }
}