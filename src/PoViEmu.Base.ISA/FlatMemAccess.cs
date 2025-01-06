namespace PoViEmu.Base.ISA
{
    public sealed class FlatMemAccess<T>
    {
        private readonly IFlatMemAccess<T> _state;

        public FlatMemAccess(IFlatMemAccess<T> state)
        {
            _state = state;
        }

        public T this[uint off]
        {
            get => _state.Get(off);
            set => _state.Set(off, value);
        }
    }
}