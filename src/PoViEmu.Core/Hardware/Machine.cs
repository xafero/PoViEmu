using System.Collections.Generic;

namespace PoViEmu.Core.Hardware
{
    public sealed class Machine
    {
        #region Data group

        /// <summary>
        /// Accumulator register
        /// </summary>
        public ushort AX;

        public byte AH
        {
            get => MachTool.GetHigh(ref AX);
            set => MachTool.SetHigh(ref AX, value);
        }

        public byte AL
        {
            get => MachTool.GetLow(ref AX);
            set => MachTool.SetLow(ref AX, value);
        }

        /// <summary>
        /// Base register
        /// </summary>
        public ushort BX;

        public byte BH
        {
            get => MachTool.GetHigh(ref BX);
            set => MachTool.SetHigh(ref BX, value);
        }

        public byte BL
        {
            get => MachTool.GetLow(ref BX);
            set => MachTool.SetLow(ref BX, value);
        }

        /// <summary>
        /// Count register
        /// </summary>
        public ushort CX;

        public byte CH
        {
            get => MachTool.GetHigh(ref CX);
            set => MachTool.SetHigh(ref CX, value);
        }

        public byte CL
        {
            get => MachTool.GetLow(ref CX);
            set => MachTool.SetLow(ref CX, value);
        }

        /// <summary>
        /// Data register
        /// </summary>
        public ushort DX;

        public byte DH
        {
            get => MachTool.GetHigh(ref DX);
            set => MachTool.SetHigh(ref DX, value);
        }

        public byte DL
        {
            get => MachTool.GetLow(ref DX);
            set => MachTool.SetLow(ref DX, value);
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

        #region Flag group

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

        private readonly byte[] _memory = MachTool.AllocateMemory();

        public IEnumerable<byte> ReadMemory(ushort segment, ushort offset, int count)
            => MachTool.Read(_memory, segment, offset, count);

        public void WriteMemory(ushort segment, ushort offset, params byte[] bytes)
            => MachTool.Write(_memory, segment, offset, bytes);

        #endregion

        #region Helpers

        public override string ToString()
        {
            return this.ToRegisterString();
        }

        #endregion
    }
}