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
using Fl = PoViEmu.Core.Hardware.Flagged;

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
        private Fl _f;

        public Fl F
        {
            get => _f;
            set => this.SetFlags(value);
        }

        /// <summary>
        /// Trap flag
        /// </summary>
        public bool TF
        {
            get => Fl.Trap.Check(ref _f);
            set => SetProperty(ref _f, Fl.Trap.Add(F, value));
        }

        /// <summary>
        /// Direction flag
        /// </summary>
        public bool DF
        {
            get => Fl.Direction.Check(ref _f);
            set => SetProperty(ref _f, Fl.Direction.Add(F, value));
        }

        /// <summary>
        /// Interrupt enable flag
        /// </summary>
        public bool IF
        {
            get => Fl.Interrupt.Check(ref _f);
            set => SetProperty(ref _f, Fl.Interrupt.Add(F, value));
        }

        /// <summary>
        /// Overflow flag
        /// </summary>
        public bool OF
        {
            get => Fl.Overflow.Check(ref _f);
            set => SetProperty(ref _f, Fl.Overflow.Add(F, value));
        }

        /// <summary>
        /// Sign flag
        /// </summary>
        public bool SF
        {
            get => Fl.Sign.Check(ref _f);
            set => SetProperty(ref _f, Fl.Sign.Add(F, value));
        }

        /// <summary>
        /// Zero flag
        /// </summary>
        public bool ZF
        {
            get => Fl.Zero.Check(ref _f);
            set => SetProperty(ref _f, Fl.Zero.Add(F, value));
        }

        /// <summary>
        /// Auxiliary carry flag
        /// </summary>
        public bool AF
        {
            get => Fl.Auxiliary.Check(ref _f);
            set => SetProperty(ref _f, Fl.Auxiliary.Add(F, value));
        }

        /// <summary>
        /// Parity flag
        /// </summary>
        public bool PF
        {
            get => Fl.Parity.Check(ref _f);
            set => SetProperty(ref _f, Fl.Parity.Add(F, value));
        }

        /// <summary>
        /// Carry flag
        /// </summary>
        public bool CF
        {
            get => Fl.Carry.Check(ref _f);
            set => SetProperty(ref _f, Fl.Carry.Add(F, value));
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

        private ushort _bank0;

        public ushort Bk0
        {
            get => _bank0;
            set => SetProperty(ref _bank0, value);
        }

        private ushort _bank1;

        public ushort Bk1
        {
            get => _bank1;
            set => SetProperty(ref _bank1, value);
        }

        private ushort _bank2;

        public ushort Bk2
        {
            get => _bank2;
            set => SetProperty(ref _bank2, value);
        }

        private ushort _bank3;

        public ushort Bk3
        {
            get => _bank3;
            set => SetProperty(ref _bank3, value);
        }

        private ushort _bank4;

        public ushort Bk4
        {
            get => _bank4;
            set => SetProperty(ref _bank4, value);
        }

        private ushort _bank5;

        public ushort Bk5
        {
            get => _bank5;
            set => SetProperty(ref _bank5, value);
        }

        private ushort _bank6;

        public ushort Bk6
        {
            get => _bank6;
            set => SetProperty(ref _bank6, value);
        }

        private ushort _frame0;

        public ushort Fr0
        {
            get => _frame0;
            set => SetProperty(ref _frame0, value);
        }

        private ushort _frame1;

        public ushort Fr1
        {
            get => _frame1;
            set => SetProperty(ref _frame1, value);
        }

        private ushort _frame2;

        public ushort Fr2
        {
            get => _frame2;
            set => SetProperty(ref _frame2, value);
        }

        private ushort _frame3;

        public ushort Fr3
        {
            get => _frame3;
            set => SetProperty(ref _frame3, value);
        }

        private ushort _frame4;

        public ushort Fr4
        {
            get => _frame4;
            set => SetProperty(ref _frame4, value);
        }

        private ushort _frame5;

        public ushort Fr5
        {
            get => _frame5;
            set => SetProperty(ref _frame5, value);
        }

        private ushort _frame6;

        public ushort Fr6
        {
            get => _frame6;
            set => SetProperty(ref _frame6, value);
        }

        private ushort _frame7;

        public ushort Fr7
        {
            get => _frame7;
            set => SetProperty(ref _frame7, value);
        }

        private ushort _frame8;

        public ushort Fr8
        {
            get => _frame8;
            set => SetProperty(ref _frame8, value);
        }

        private ushort _frame9;

        public ushort Fr9
        {
            get => _frame9;
            set => SetProperty(ref _frame9, value);
        }

        private ushort _frame10;

        public ushort Fr10
        {
            get => _frame10;
            set => SetProperty(ref _frame10, value);
        }

        private ushort _frame11;

        public ushort Fr11
        {
            get => _frame11;
            set => SetProperty(ref _frame11, value);
        }

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
                    nameof(AH) => AH,
                    nameof(BH) => BH,
                    nameof(CH) => CH,
                    nameof(DH) => DH,
                    nameof(AL) => AL,
                    nameof(BL) => BL,
                    nameof(CL) => CL,
                    nameof(DL) => DL,
                    nameof(SI) => SI,
                    nameof(DI) => DI,
                    nameof(DS) => DS,
                    nameof(ES) => ES,
                    nameof(SS) => SS,
                    nameof(SP) => SP,
                    nameof(BP) => BP,
                    nameof(CS) => CS,
                    nameof(IP) => IP,
                    nameof(F) => F,
                    nameof(TF) => TF,
                    nameof(DF) => DF,
                    nameof(IF) => IF,
                    nameof(OF) => OF,
                    nameof(SF) => SF,
                    nameof(ZF) => ZF,
                    nameof(AF) => AF,
                    nameof(PF) => PF,
                    nameof(CF) => CF,
                    nameof(Bk0) => Bk0,
                    nameof(Bk1) => Bk1,
                    nameof(Bk2) => Bk2,
                    nameof(Bk3) => Bk3,
                    nameof(Bk4) => Bk4,
                    nameof(Bk5) => Bk5,
                    nameof(Bk6) => Bk6,
                    nameof(Fr0) => Fr0,
                    nameof(Fr1) => Fr1,
                    nameof(Fr2) => Fr2,
                    nameof(Fr3) => Fr3,
                    nameof(Fr4) => Fr4,
                    nameof(Fr5) => Fr5,
                    nameof(Fr6) => Fr6,
                    nameof(Fr7) => Fr7,
                    nameof(Fr8) => Fr8,
                    nameof(Fr9) => Fr9,
                    nameof(Fr10) => Fr10,
                    nameof(Fr11) => Fr11,
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