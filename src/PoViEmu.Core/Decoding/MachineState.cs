using System.Collections.Generic;

// ReSharper disable UnassignedField.Global
// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Decoding
{
    public struct MachineState
    {
        /// <summary>
        /// Accumulator Register
        /// </summary>
        public ushort AX = 0;

        /// <summary>
        /// Base Register
        /// </summary>
        public ushort BX = 0;

        /// <summary>
        /// Count Register
        /// </summary>
        public ushort CX = 0;

        /// <summary>
        /// Data Register
        /// </summary>
        public ushort DX = 0;

        /// <summary>
        /// Source Index         
        /// </summary>
        public ushort SI = 0;

        /// <summary>
        /// Destination Index 
        /// </summary>
        public ushort DI = 0;

        /// <summary>
        /// Data Segment           
        /// </summary>
        public ushort DS = 0;

        /// <summary>
        /// Extra Segment 
        /// </summary>
        public ushort ES = 0;

        /// <summary>
        /// Stack Segment              
        /// </summary>
        public ushort SS = 0;

        /// <summary>
        /// Stack Pointer    
        /// </summary>
        public ushort SP = 0;

        /// <summary>
        /// Base Pointer             
        /// </summary>
        public ushort BP = 0;

        /// <summary>
        /// Code Segment               
        /// </summary>
        public ushort CS = 0;

        /// <summary>
        /// Instruction Pointer     
        /// </summary>
        public ushort IP = 0;

        /// <summary>
        /// Carry Flag
        /// </summary>
        public bool CF = false;

        /// <summary>
        /// Zero Flag
        /// </summary>
        public bool ZF = false;

        /// <summary>
        /// Sign Flag
        /// </summary>
        public bool SF = false;

        /// <summary>
        /// Direction Flag
        /// </summary>
        public bool DF = false;

        /// <summary>
        /// Interrupt Enable Flag
        /// </summary>
        public bool IF = false;

        /// <summary>
        /// Overflow Flag
        /// </summary>
        public bool OF = false;

        /// <summary>
        /// Parity Flag
        /// </summary>
        public bool PF = false;

        /// <summary>
        /// Auxiliary Carry Flag
        /// </summary>
        public bool AF = false;

        /* Ems */
        public ushort Bank0 = 0;
        public ushort Bank1 = 0;
        public ushort Bank2 = 0;
        public ushort Bank3 = 0;
        public ushort Bank4 = 0;
        public ushort Bank5 = 0;
        public ushort Bank6 = 0;

        public ushort Frame0 = 0;
        public ushort Frame1 = 0;
        public ushort Frame2 = 0;
        public ushort Frame3 = 0;
        public ushort Frame4 = 0;
        public ushort Frame5 = 0;
        public ushort Frame6 = 0;
        public ushort Frame7 = 0;
        public ushort Frame8 = 0;
        public ushort Frame9 = 0;
        public ushort Frame10 = 0;
        public ushort Frame11 = 0;

        /// <summary>
        /// The stack
        /// </summary>
        public Dictionary<ushort, List<ushort>> Stack;

        /// <summary>
        /// The memory
        /// </summary>
        public Dictionary<ushort, IDictionary<ushort, List<byte>>> Memory;

        public MachineState()
        {
            Stack = new();
            Memory = new();
        }

        public override string ToString() => this.ToRegisterString(" ");
    }
}