using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PoViEmu.Core.Decoding.Ops.Regs;
using PoViEmu.Core.Hardware.AckNow;
using static PoViEmu.Core.Hardware.MachTool;

namespace PoViEmu.Core.Hardware
{
    public sealed class MachineState
    {
        #region Data group

        /// <summary>
        /// Accumulator register
        /// </summary>
        public ushort AX;

        public byte AH
        {
            get => GetHigh(ref AX);
            set => SetHigh(ref AX, value);
        }

        public byte AL
        {
            get => GetLow(ref AX);
            set => SetLow(ref AX, value);
        }

        /// <summary>
        /// Base register
        /// </summary>
        public ushort BX;

        public byte BH
        {
            get => GetHigh(ref BX);
            set => SetHigh(ref BX, value);
        }

        public byte BL
        {
            get => GetLow(ref BX);
            set => SetLow(ref BX, value);
        }

        /// <summary>
        /// Count register
        /// </summary>
        public ushort CX;

        public byte CH
        {
            get => GetHigh(ref CX);
            set => SetHigh(ref CX, value);
        }

        public byte CL
        {
            get => GetLow(ref CX);
            set => SetLow(ref CX, value);
        }

        /// <summary>
        /// Data register
        /// </summary>
        public ushort DX;

        public byte DH
        {
            get => GetHigh(ref DX);
            set => SetHigh(ref DX, value);
        }

        public byte DL
        {
            get => GetLow(ref DX);
            set => SetLow(ref DX, value);
        }

        #endregion

        #region Pointer and index group

        /// <summary>
        /// Stack pointer
        /// </summary>
        public ushort SP;

        /// <summary>
        /// Base pointer
        /// </summary>
        public ushort BP;

        /// <summary>
        /// Instruction pointer
        /// </summary>
        public ushort IP;

        /// <summary>
        /// Source index
        /// </summary>
        public ushort SI;

        /// <summary>
        /// Destination index
        /// </summary>
        public ushort DI;

        #endregion

        #region Control flags

        /// <summary>
        /// Trap flag
        /// </summary>
        public bool TF;

        /// <summary>
        /// Direction flag
        /// </summary>
        public bool DF;

        /// <summary>
        /// Interrupt enable flag
        /// </summary>
        public bool IF;

        #endregion

        #region Status flags

        /// <summary>
        /// Overflow flag
        /// </summary>
        public bool OF;

        /// <summary>
        /// Sign flag
        /// </summary>
        public bool SF;

        /// <summary>
        /// Zero flag
        /// </summary>
        public bool ZF;

        /// <summary>
        /// Auxiliary carry flag
        /// </summary>
        public bool AF;

        /// <summary>
        /// Parity flag
        /// </summary>
        public bool PF;

        /// <summary>
        /// Carry flag
        /// </summary>
        public bool CF;

        #endregion

        #region Segment group

        /// <summary>
        /// Code segment register
        /// </summary>
        public ushort CS;

        /// <summary>
        /// Data segment register
        /// </summary>
        public ushort DS;

        /// <summary>
        /// Stack segment register
        /// </summary>
        public ushort SS;

        /// <summary>
        /// Extra segment register
        /// </summary>
        public ushort ES;

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
        #endregion
    }
}