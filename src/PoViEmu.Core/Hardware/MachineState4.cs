using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace PoViEmu.Core.Hardware
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public sealed class MachineState4 : INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region Data group
        private ushort _ax;

        public ushort AX
        {
            get => _ax;
            set => SetProperty(ref _ax, value);
        }
        #endregion

        #region Changing events
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;

        [DebuggerNonUserCode]
        [ExcludeFromCodeCoverage]
        private void OnPropertyChanging([CallerMemberName] string? name = null)
        {
            var evt = new PropertyChangingEventArgs(name);
            PropertyChanging?.Invoke(this, evt);
        }

        [DebuggerNonUserCode]
        [ExcludeFromCodeCoverage]
        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            var evt = new PropertyChangedEventArgs(name);
            PropertyChanged?.Invoke(this, evt);
        }

        [DebuggerNonUserCode]
        [ExcludeFromCodeCoverage]
        private void SetProperty<T>(ref T field, T newValue,
            [CallerMemberName] string? name = null, IEqualityComparer<T>? comparer = null)
        {
            if ((comparer ?? EqualityComparer<T>.Default).Equals(field, newValue))
                return;
            OnPropertyChanging(name);
            field = newValue;
            OnPropertyChanged(name);
        }
        #endregion

        #region Indexers
        public object this[string? name]
        {
            get
            {
                return name switch
                {
                    nameof(AX) => AX,
                    _ => throw new InvalidOperationException(name)
                };
            }
        }
        #endregion
    }
}