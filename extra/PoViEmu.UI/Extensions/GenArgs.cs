using System;

namespace PoViEmu.UI.Extensions
{
    public sealed class GenArgs<T> : EventArgs
    {
        public GenArgs(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
}