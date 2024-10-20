using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using PoViEmu.Core.Decoding.Ops.Regs;
using PoViEmu.Core.Hardware.AckNow;
using static PoViEmu.Core.Hardware.MachTool;

namespace PoViEmu.Core.Hardware
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public sealed class MachineState : INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region Data group

        /// <summary>
        /// Accumulator register
        /// </summary>
        private ushort _ax;

        public ushort AX
        {
            get => _ax;
            set => SetProperty(ref _ax, value);
        }

        public byte AH
        {
            get => GetHigh(AX);
            set => AX = SetHigh(_ax, value);
        }

        public byte AL
        {
            get => GetLow(AX);
            set => AX = SetLow(_ax, value);
        }

        /// <summary>
        /// Base register
        /// </summary>
        private ushort _bx;

        public ushort BX
        {
            get => _bx;
            set => SetProperty(ref _bx, value);
        }

        public byte BH
        {
            get => GetHigh(BX);
            set => BX = SetHigh(_bx, value);
        }

        public byte BL
        {
            get => GetLow(BX);
            set => BX = SetLow(_bx, value);
        }

        /// <summary>
        /// Count register
        /// </summary>
        private ushort _cx;

        public ushort CX
        {
            get => _cx;
            set => SetProperty(ref _cx, value);
        }

        public byte CH
        {
            get => GetHigh(CX);
            set => CX = SetHigh(_cx, value);
        }

        public byte CL
        {
            get => GetLow(CX);
            set => CX = SetLow(_cx, value);
        }

        /// <summary>
        /// Data register
        /// </summary>
        private ushort _dx;

        public ushort DX
        {
            get => _dx;
            set => SetProperty(ref _dx, value);
        }

        public byte DH
        {
            get => GetHigh(DX);
            set => DX = SetHigh(_dx, value);
        }

        public byte DL
        {
            get => GetLow(DX);
            set => DX = SetLow(_dx, value);
        }

        #endregion

        #region Pointer and index group

        /// <summary>
        /// Stack pointer
        /// </summary>
        private ushort _sp;

        public ushort SP
        {
            get => _sp;
            set => SetProperty(ref _sp, value);
        }

        /// <summary>
        /// Base pointer
        /// </summary>
        private ushort _bp;

        public ushort BP
        {
            get => _bp;
            set => SetProperty(ref _bp, value);
        }

        /// <summary>
        /// Instruction pointer
        /// </summary>
        private ushort _ip;

        public ushort IP
        {
            get => _ip;
            set => SetProperty(ref _ip, value);
        }

        /// <summary>
        /// Source index
        /// </summary>
        private ushort _si;

        public ushort SI
        {
            get => _si;
            set => SetProperty(ref _si, value);
        }

        /// <summary>
        /// Destination index
        /// </summary>
        private ushort _di;

        public ushort DI
        {
            get => _di;
            set => SetProperty(ref _di, value);
        }

        #endregion

        #region Internal flags

        /// <summary>
        /// Flag register
        /// </summary>
        public Flagged F;

        /// <summary>
        /// Trap flag
        /// </summary>
        public bool TF
        {
            get => Flagged.Trap.Check(ref F);
            set => Flagged.Trap.Apply(ref F, value);
        }

        /// <summary>
        /// Direction flag
        /// </summary>
        public bool DF
        {
            get => Flagged.Direction.Check(ref F);
            set => Flagged.Direction.Apply(ref F, value);
        }

        /// <summary>
        /// Interrupt enable flag
        /// </summary>
        public bool IF
        {
            get => Flagged.Interrupt.Check(ref F);
            set => Flagged.Interrupt.Apply(ref F, value);
        }

        /// <summary>
        /// Overflow flag
        /// </summary>
        public bool OF
        {
            get => Flagged.Overflow.Check(ref F);
            set => Flagged.Overflow.Apply(ref F, value);
        }

        /// <summary>
        /// Sign flag
        /// </summary>
        public bool SF
        {
            get => Flagged.Sign.Check(ref F);
            set => Flagged.Sign.Apply(ref F, value);
        }

        /// <summary>
        /// Zero flag
        /// </summary>
        public bool ZF
        {
            get => Flagged.Zero.Check(ref F);
            set => Flagged.Zero.Apply(ref F, value);
        }

        /// <summary>
        /// Auxiliary carry flag
        /// </summary>
        public bool AF
        {
            get => Flagged.Auxiliary.Check(ref F);
            set => Flagged.Auxiliary.Apply(ref F, value);
        }

        /// <summary>
        /// Parity flag
        /// </summary>
        public bool PF
        {
            get => Flagged.Parity.Check(ref F);
            set => Flagged.Parity.Apply(ref F, value);
        }

        /// <summary>
        /// Carry flag
        /// </summary>
        public bool CF
        {
            get => Flagged.Carry.Check(ref F);
            set => Flagged.Carry.Apply(ref F, value);
        }

        #endregion

        #region Segment group

        /// <summary>
        /// Code segment register
        /// </summary>
        private ushort _cs;

        public ushort CS
        {
            get => _cs;
            set => SetProperty(ref _cs, value);
        }

        /// <summary>
        /// Data segment register
        /// </summary>
        private ushort _ds;

        public ushort DS
        {
            get => _ds;
            set => SetProperty(ref _ds, value);
        }

        /// <summary>
        /// Stack segment register
        /// </summary>
        private ushort _ss;

        public ushort SS
        {
            get => _ss;
            set => SetProperty(ref _ss, value);
        }

        /// <summary>
        /// Extra segment register
        /// </summary>
        private ushort _es;

        public ushort ES
        {
            get => _es;
            set => SetProperty(ref _es, value);
        }

        #endregion

        #region Expanded Memory Specification

        public ushort Bank0;
        public ushort Bank1;
        public ushort Bank2;
        public ushort Bank3;
        public ushort Bank4;
        public ushort Bank5;
        public ushort Bank6;

        public ushort Frame0;
        public ushort Frame1;
        public ushort Frame2;
        public ushort Frame3;
        public ushort Frame4;
        public ushort Frame5;
        public ushort Frame6;
        public ushort Frame7;
        public ushort Frame8;
        public ushort Frame9;
        public ushort Frame10;
        public ushort Frame11;

        #endregion

        #region Memory group

        private readonly byte[] _memory = AllocateMemory();

        public IEnumerable<byte> ReadMemory(ushort segment, ushort offset, int count)
            => Read(_memory, segment, offset, count);

        public void WriteMemory(ushort segment, ushort offset, params byte[] bytes)
            => Write(_memory, segment, offset, bytes);

        public byte[] this[ushort segment, ushort offset]
        {
            get => ReadMemory(segment, offset, 128 * 4).ToArray();
            set => WriteMemory(segment, offset, value);
        }

        #endregion

        #region Helpers

        public override string ToString()
        {
            return this.ToRegisterString(" ");
        }

        #endregion

        #region Indexers

        public ushort this[B16Register reg16]
        {
            get => this.Get(reg16);
            set => this.Set(reg16, value);
        }

        public ushort this[Reg16Operand reg16]
        {
            get => this.Get(reg16);
            set => this.Set(reg16, value);
        }

        public byte this[Reg8Operand reg8]
        {
            get => this.Get(reg8);
            set => this.Set(reg8, value);
        }

        [JsonIgnore]
        public ushort TopOfStack
        {
            get => this.Pop();
            set => this.Push(value);
        }

        public object this[string? name]
        {
            get
            {
                return name switch
                {
                    nameof(AX) => AX,
                    nameof(BX) => BX,
                    nameof(CX) => CX,
                    nameof(DX) => DX,
                    nameof(SI) => SI,
                    nameof(DI) => DI,
                    nameof(DS) => DS,
                    nameof(ES) => ES,
                    nameof(SS) => SS,
                    nameof(SP) => SP,
                    nameof(BP) => BP,
                    nameof(CS) => CS,
                    nameof(IP) => IP,
                    _ => throw new InvalidOperationException(name)
                };
            }
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
        private void SetProperty<T>(ref T field, T newValue, [CallerMemberName] string? name = null,
            IEqualityComparer<T>? comparer = null)
        {
            if ((comparer ?? EqualityComparer<T>.Default).Equals(field, newValue))
                return;
            OnPropertyChanging(name);
            field = newValue;
            OnPropertyChanged(name);
        }

        #endregion
    }
}