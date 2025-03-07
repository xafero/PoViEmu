using System.Collections.Generic;

namespace PoViEmu.Base
{
    public sealed class RingStack<T>
    {
        private readonly int _capacity;
        private readonly List<T> _list;

        public RingStack(int capacity)
        {
            _list = new List<T>(_capacity = capacity);
        }

        public void Push(T model)
        {
            while (_list.Count >= _capacity)
            {
                _list.RemoveAt(0);
            }
            _list.Add(model);
        }

        private int? LastIndex
            => _list.Count > 0 ? _list.Count - 1 : null;

        public int Count
            => _list.Count;

        public T? Peek()
            => LastIndex is { } index ? _list[index] : default;

        public T? Pop()
        {
            if (LastIndex is not { } index)
                return default;

            var last = _list[index];
            _list.RemoveAt(index);
            return last;
        }
    }
}