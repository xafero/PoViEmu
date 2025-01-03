using System.ComponentModel;

namespace PoViEmu.Base.CPU.Diff
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