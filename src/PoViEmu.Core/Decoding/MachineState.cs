using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable UnassignedField.Global
// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Decoding
{
    public struct MachineState
    {
        /// <summary>
        /// Accumulator Register
        /// </summary>
        public ushort AX;

        /// <summary>
        /// Base Register
        /// </summary>
        public ushort BX;

        /// <summary>
        /// Count Register
        /// </summary>
        public ushort CX;

        /// <summary>
        /// Data Register
        /// </summary>
        public ushort DX;

        /// <summary>
        /// Source Index         
        /// </summary>
        public ushort SI;

        /// <summary>
        /// Destination Index 
        /// </summary>
        public ushort DI;

        /// <summary>
        /// Data Segment           
        /// </summary>
        public ushort DS;

        /// <summary>
        /// Extra Segment 
        /// </summary>
        public ushort ES;

        /// <summary>
        /// Stack Segment              
        /// </summary>
        public ushort SS;

        /// <summary>
        /// Stack Pointer    
        /// </summary>
        public ushort SP;

        /// <summary>
        /// Base Pointer             
        /// </summary>
        public ushort BP;

        /// <summary>
        /// Code Segment               
        /// </summary>
        public ushort CS;

        /// <summary>
        /// Instruction Pointer     
        /// </summary>
        public ushort IP;

        /// <summary>
        /// Carry Flag
        /// </summary>
        public bool CF;

        /// <summary>
        /// Zero Flag
        /// </summary>
        public bool ZF;

        /// <summary>
        /// Sign Flag
        /// </summary>
        public bool SF;

        /// <summary>
        /// Direction Flag
        /// </summary>
        public bool DF;

        /// <summary>
        /// Interrupt Enable Flag
        /// </summary>
        public bool IF;

        /// <summary>
        /// Overflow Flag
        /// </summary>
        public bool OF;

        /// <summary>
        /// Parity Flag
        /// </summary>
        public bool PF;

        /// <summary>
        /// Auxiliary Carry Flag
        /// </summary>
        public bool AF;

        /* Ems */
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

        /// <summary>
        /// The stack
        /// </summary>
        public Dictionary<ushort, List<ushort>> Stack;

        /// <summary>
        /// The memory
        /// </summary>
        public Dictionary<ushort, IDictionary<ushort, List<byte>>> Memory;

        /* For debugging */
        public string ToMemoryString() => this.ToMemoryString(Environment.NewLine);
        public string ToCodeString() => this.ToCodeString(Environment.NewLine);
        public string ToStackString() => this.ToStackString(" ");

        public override string ToString() => ToString(" ");

        public string ToString(string sep)
        {
            var stack = string.Join(",", Stack?.Select(x =>
                $"SS:{x.Key:X4} #{x.Value.Count * 2}") ?? []);
            var memory = string.Join(",", Memory?.SelectMany(x =>
                x.Value.Select(k => $"{x.Key:X4}:{k.Key:X4} #{k.Value.Count}")) ?? []);
            return $"AX={AX:x4}{sep}BX={BX:x4}{sep}CX={CX:x4}{sep}DX={DX:x4}{sep}" +
                   $"SI={SI:x4}{sep}DI={DI:x4}{sep}DS={DS:x4}{sep}ES={ES:x4}{sep}" +
                   $"SS={SS:x4}{sep}SP={SP:x4}{sep}BP={BP:x4}{sep}CS={CS:x4}{sep}" +
                   $"IP={IP:x4}{sep}CF={(CF ? 1 : 0)}{sep}ZF={(ZF ? 1 : 0)}{sep}" +
                   $"SF={(SF ? 1 : 0)}{sep}DF={(DF ? 1 : 0)}{sep}" +
                   $"IF={(IF ? 1 : 0)}{sep}OF={(OF ? 1 : 0)}{sep}" +
                   $"PF={(PF ? 1 : 0)}{sep}AF={(AF ? 1 : 0)}{sep}" +
                   $"B0={Bank0:x4}{sep}B1={Bank1:x4}{sep}B2={Bank2:x4}{sep}" +
                   $"B3={Bank3:x4}{sep}B4={Bank4:x4}{sep}B5={Bank5:x4}{sep}" +
                   $"B6={Bank6:x4}{sep}F0={Frame0:x4}{sep}F1={Frame1:x4}{sep}" +
                   $"F2={Frame2:x4}{sep}F3={Frame3:x4}{sep}F4={Frame4:x4}{sep}" +
                   $"F5={Frame5:x4}{sep}F6={Frame6:x4}{sep}F7={Frame7:x4}{sep}" +
                   $"F8={Frame8:x4}{sep}F9={Frame9:x4}{sep}F10={Frame10:x4}{sep}" +
                   $"F11={Frame11:x4}{sep}Stack=({stack}){sep}Memory=({memory})";
        }
    }
}