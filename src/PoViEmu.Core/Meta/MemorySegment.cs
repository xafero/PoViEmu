namespace PoViEmu.Core.Meta
{
    /// <summary>
    /// One segment (64 Kbytes) between offset addresses 0000 and FFFF
    /// </summary>
    public sealed class MemorySegment
    {
        private readonly byte[] _array;
        private readonly bool[] _dirty;

        public MemorySegment()
        {
            _array = new byte[64 * 1024];
            _dirty = new bool[_array.Length];
        }

        public byte this[ushort index]
        {
            get => _array[index];
            set
            {
                _dirty[index] = true;
                _array[index] = value;
            }
        }
    }
}