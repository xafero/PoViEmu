using System;
using System.Linq;
using Iced.Intel;
using PoViEmu.Core.Decoding.Ops;

namespace PoViEmu.Core.Hardware.Errors
{
    public sealed class UnhandledOpcodeException : Exception
    {
        public UnhandledOpcodeException(Instruction instruct, BaseOperand[] ops)
            : base($"({instruct.Mnemonic}) {instruct}\n[{string.Join(" ; ", ops.Select(o => o.ToString()))}]")
        {
        }
    }
}