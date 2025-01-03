using System;
using System.Linq;
using Iced.Intel;
using PoViEmu.I186.ISA.Ops;

namespace PoViEmu.I186.CPU.Errors
{
    public sealed class UnhandledOpcodeException : Exception
    {
        public UnhandledOpcodeException(Instruction instruct, BaseOperand[] ops)
            : base($"({instruct.Mnemonic}) {instruct}\n[{string.Join(" ; ", ops.Select(o => o.ToString()))}]")
        {
        }
    }
}