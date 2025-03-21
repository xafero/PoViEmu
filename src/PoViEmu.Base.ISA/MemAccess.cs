namespace PoViEmu.Base.ISA
{
    public sealed class MemAccess<T>
    {
        private readonly IMemAccess<T> _state;

        public MemAccess(IMemAccess<T> state)
        {
            _state = state;
        }

        public T this[ushort seg, ushort off]
        {
            get => _state.Get(seg, off);
            set => _state.Set(seg, off, value);
        }
    }
}