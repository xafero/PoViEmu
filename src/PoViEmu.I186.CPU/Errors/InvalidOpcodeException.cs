using System;
using PoViEmu.I186.ISA.Decoding;

namespace PoViEmu.I186.CPU.Errors
{
    public sealed class InvalidOpcodeException : Exception
    {
        public InvalidOpcodeException(XInstruction instruct) : base(instruct.ToString())
        {
        }
    }
}