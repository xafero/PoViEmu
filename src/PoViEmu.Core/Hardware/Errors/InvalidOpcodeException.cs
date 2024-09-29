using System;
using PoViEmu.Core.Decoding;

namespace PoViEmu.Core.Hardware.Errors
{
    public sealed class InvalidOpcodeException : Exception
    {
        public InvalidOpcodeException(XInstruction instruct) : base(instruct.ToString())
        {
        }
    }
}