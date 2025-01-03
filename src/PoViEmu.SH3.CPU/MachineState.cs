using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using PoViEmu.Base.CPU;
using PoViEmu.SH3.ISA;
using static PoViEmu.SH3.CPU.SegTool;
using static PoViEmu.Base.CPU.MemTool;
using Fl = PoViEmu.SH3.ISA.Flagged;

namespace PoViEmu.SH3.CPU
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public sealed class MachineState : INotifyPropertyChanging, INotifyPropertyChanged,
        IState
    {
        #region General registers

        private uint _r0;

        public uint R0
        {
            get => _r0;
            set => SetProperty(ref _r0, value);
        }

        private uint _r1;

        public uint R1
        {
            get => _r1;
            set => SetProperty(ref _r1, value);
        }

        private uint _r2;

        public uint R2
        {
            get => _r2;
            set => SetProperty(ref _r2, value);
        }

        private uint _r3;

        public uint R3
        {
            get => _r3;
            set => SetProperty(ref _r3, value);
        }

        private uint _r4;

        public uint R4
        {
            get => _r4;
            set => SetProperty(ref _r4, value);
        }

        private uint _r5;

        public uint R5
        {
            get => _r5;
            set => SetProperty(ref _r5, value);
        }

        private uint _r6;

        public uint R6
        {
            get => _r6;
            set => SetProperty(ref _r6, value);
        }

        private uint _r7;

        public uint R7
        {
            get => _r7;
            set => SetProperty(ref _r7, value);
        }

        private uint _r8;

        public uint R8
        {
            get => _r8;
            set => SetProperty(ref _r8, value);
        }

        private uint _r9;

        public uint R9
        {
            get => _r9;
            set => SetProperty(ref _r9, value);
        }

        private uint _r10;

        public uint R10
        {
            get => _r10;
            set => SetProperty(ref _r10, value);
        }

        private uint _r11;

        public uint R11
        {
            get => _r11;
            set => SetProperty(ref _r11, value);
        }

        private uint _r12;

        public uint R12
        {
            get => _r12;
            set => SetProperty(ref _r12, value);
        }

        private uint _r13;

        public uint R13
        {
            get => _r13;
            set => SetProperty(ref _r13, value);
        }

        private uint _r14;

        public uint R14
        {
            get => _r14;
            set => SetProperty(ref _r14, value);
        }

        private uint _r15;

        public uint R15
        {
            get => _r15;
            set => SetProperty(ref _r15, value);
        }

        #endregion

        #region Banked registers

        private uint _r0b;

        public uint R0_b
        {
            get => _r0b;
            set => SetProperty(ref _r0b, value);
        }

        private uint _r1b;

        public uint R1_b
        {
            get => _r1b;
            set => SetProperty(ref _r1b, value);
        }

        private uint _r2b;

        public uint R2_b
        {
            get => _r2b;
            set => SetProperty(ref _r2b, value);
        }

        private uint _r3b;

        public uint R3_b
        {
            get => _r3b;
            set => SetProperty(ref _r3b, value);
        }

        private uint _r4b;

        public uint R4_b
        {
            get => _r4b;
            set => SetProperty(ref _r4b, value);
        }

        private uint _r5b;

        public uint R5_b
        {
            get => _r5b;
            set => SetProperty(ref _r5b, value);
        }

        private uint _r6b;

        public uint R6_b
        {
            get => _r6b;
            set => SetProperty(ref _r6b, value);
        }

        private uint _r7b;

        public uint R7_b
        {
            get => _r7b;
            set => SetProperty(ref _r7b, value);
        }

        #endregion

        #region Special registers

        private uint _gbr;

        /// <summary>
        /// Global Base Register
        /// </summary>
        public uint GBR
        {
            get => _gbr;
            set => SetProperty(ref _gbr, value);
        }

        private uint _mach;

        /// <summary>
        /// Multiply-Accumulate High Register
        /// </summary>
        public uint MACH
        {
            get => _mach;
            set => SetProperty(ref _mach, value);
        }

        private uint _macl;

        /// <summary>
        /// Multiply-Accumulate Low Register
        /// </summary>
        public uint MACL
        {
            get => _macl;
            set => SetProperty(ref _macl, value);
        }

        private uint _pr;

        /// <summary>
        /// Procedure Register
        /// </summary>
        public uint PR
        {
            get => _pr;
            set => SetProperty(ref _pr, value);
        }

        private uint _pc;

        /// <summary>
        /// Program Counter
        /// </summary>
        public uint PC
        {
            get => _pc;
            set => SetProperty(ref _pc, value);
        }

        private uint _vbr;

        /// <summary>
        /// Vector Base Register
        /// </summary>
        public uint VBR
        {
            get => _vbr;
            set => SetProperty(ref _vbr, value);
        }

        private uint _spc;

        /// <summary>
        /// Saved Program Counter
        /// </summary>
        public uint SPC
        {
            get => _spc;
            set => SetProperty(ref _spc, value);
        }

        private uint _ssr;

        /// <summary>
        /// Saved Status Register
        /// </summary>
        public uint SSR
        {
            get => _ssr;
            set => SetProperty(ref _ssr, value);
        }

        #endregion

        #region Control flags

        private Fl _sr;

        /// <summary>
        /// Status Register
        /// </summary>
        public Fl SR
        {
            get => _sr;
            set => this.SetFlags(value);
        }

        /// <summary>
        /// Multiplier flag
        /// (used by DIVOS/DIVOU and DIV1 instructions)
        /// </summary>
        public bool M
        {
            get => Fl.M_bit.Check(ref _sr);
            set => SetProperty(ref _sr, Fl.M_bit.Add(SR, value));
        }

        /// <summary>
        /// Quotient flag
        /// (used by DIVOS/DIVOU and DIV1 instructions)
        /// </summary>
        public bool Q
        {
            get => Fl.Q_bit.Check(ref _sr);
            set => SetProperty(ref _sr, Fl.Q_bit.Add(SR, value));
        }

        /// <summary>
        /// Sign flag
        /// (used by MAC instruction)
        /// </summary>
        public bool S
        {
            get => Fl.S_bit.Check(ref _sr);
            set => SetProperty(ref _sr, Fl.S_bit.Add(SR, value));
        }

        /// <summary>
        /// Test flag
        /// (used by various instructions to indicate true/false or carry/borrow/overflow)
        /// </summary>
        public bool T
        {
            get => Fl.T_bit.Check(ref _sr);
            set => SetProperty(ref _sr, Fl.T_bit.Add(SR, value));
        }

        /// <summary>
        /// Interrupt mask bit 3
        /// </summary>
        public bool I3
        {
            get => Fl.I3.Check(ref _sr);
            set => SetProperty(ref _sr, Fl.I3.Add(SR, value));
        }

        /// <summary>
        /// Interrupt mask bit 2
        /// </summary>
        public bool I2
        {
            get => Fl.I2.Check(ref _sr);
            set => SetProperty(ref _sr, Fl.I2.Add(SR, value));
        }

        /// <summary>
        /// Interrupt mask bit 1
        /// </summary>
        public bool I1
        {
            get => Fl.I1.Check(ref _sr);
            set => SetProperty(ref _sr, Fl.I1.Add(SR, value));
        }

        /// <summary>
        /// Interrupt mask bit 0
        /// </summary>
        public bool I0
        {
            get => Fl.I0.Check(ref _sr);
            set => SetProperty(ref _sr, Fl.I0.Add(SR, value));
        }

        /// <summary>
        /// Block bit
        /// (1 = Mask exceptions/interrupts, 0 = Accept exceptions/interrupts)
        /// </summary>
        public bool BL
        {
            get => Fl.Block_bit.Check(ref _sr);
            set => SetProperty(ref _sr, Fl.Block_bit.Add(SR, value));
        }

        /// <summary>
        /// Register bank bit
        /// (1 = Access bank in privileged mode, 0 = Default)
        /// </summary>
        public bool RB
        {
            get => Fl.Bank_bit.Check(ref _sr);
            set => SetProperty(ref _sr, Fl.Bank_bit.Add(SR, value));
        }

        /// <summary>
        /// Processor operation mode bit
        /// (1 = Privileged mode, 0 = User mode)
        /// </summary>
        public bool MD
        {
            get => Fl.Mode_bit.Check(ref _sr);
            set => SetProperty(ref _sr, Fl.Mode_bit.Add(SR, value));
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

        #region Indexers

        public object this[string? name]
        {
            get => this.GetByString(name);
            set => this.SetByString(name, value);
        }

        #endregion

        #region Memory group

        private readonly byte[] _memory = AllocateMemory();

        public IEnumerable<byte> ReadMemory(uint offset, int count)
            => Read(_memory, offset, count);

        public void WriteMemory(uint offset, params byte[] bytes)
            => Write(_memory, offset, bytes);

        #endregion

        #region Helpers

        public override string ToString()
        {
            return this.ToRegisterString(" ");
        }

        #endregion
    }
}