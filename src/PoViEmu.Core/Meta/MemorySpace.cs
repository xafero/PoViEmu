using System.Collections.Generic;

namespace PoViEmu.Core.Meta
{
    /// <summary>
    /// One address space (20-bit) for multiple memory segments
    /// </summary>
    public sealed class MemorySpace
    {
        private readonly SortedDictionary<int, MemorySegment> _space;

        public MemorySpace()
        {
            _space = new SortedDictionary<int, MemorySegment>();
        }

        public MemorySegment this[int index]
        {
            get => _space[index];
            set => _space[index] = value;
        }
    }
}