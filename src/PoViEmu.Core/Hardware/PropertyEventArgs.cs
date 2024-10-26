using System.ComponentModel;

namespace PoViEmu.Core.Hardware
{
    public sealed class PropertyEventArgs : PropertyChangedEventArgs
    {
        public PropertyEventArgs(string? name, (object? oldVal, object newVal) pair)
            : base(name)
        {
            Old = pair.oldVal;
            New = pair.newVal;
        }

        public object? Old { get;   }
        public object? New { get;   }
    }
}