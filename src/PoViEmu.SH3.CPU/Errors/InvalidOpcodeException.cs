using System;
using PoViEmu.SH3.ISA.Decoding;

namespace PoViEmu.SH3.CPU.Errors
{
    public sealed class InvalidOpcodeException : Exception
    {
        public InvalidOpcodeException(XInstruction instruct) : base(instruct.ToString())
        {
        }
    }
}