using System.IO;

namespace PoViEmu.SH3.ISA.Decoding
{
    public sealed class ArrayReader : IByteReader
    {
        private readonly byte[] _bytes;
        private int _index;

        public ArrayReader(byte[] bytes)
        {
            _bytes = bytes;
            _index = -1;
        }

        public byte ReadByte()
        {
            _index++;
            if (_index >= _bytes.Length)
                throw new EndOfStreamException();
            var raw = _bytes[_index];
            return raw;
        }
    }
}