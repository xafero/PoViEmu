// ReSharper disable InconsistentNaming

using System.Linq;
using Iced.Intel;
using PoViEmu.Core.Decoding;
using PoViEmu.Core.Decoding.Ops;
using PoViEmu.Core.Hardware.Errors;

namespace PoViEmu.Core.Hardware
{
    public sealed class NC3022
    {
        public void Execute(XInstruction instruct, MachineState m)
        {
            var parsed = instruct.Parsed;
            if (parsed.IsInvalid)
            {
                throw new InvalidOpcodeException(instruct);
            }
            var ops = parsed.GetOps().ToArray();
            switch (parsed.Mnemonic)
            {
                case Mnemonic.Mov:
                    if (ops is [RegOperand movR1, U16Operand movI1])
                    {
                        ref var movT = ref m.GetRef(movR1);
                        var movV = movI1.Val;
                        movT = movV;
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [RegOperand movR2, RegOperand movR3])
                    {
                        ref var movT = ref m.GetRef(movR2);
                        var movV = m.GetRef(movR3);
                        movT = movV;
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Push:
                    if (ops is [RegOperand pushR])
                    {
                        ref var pushS = ref m.GetRef(pushR);
                        m.Push(pushS);
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Pop:
                    if (ops is [RegOperand popR])
                    {
                        ref var popT = ref m.GetRef(popR);
                        popT = m.Pop();
                        SetIp(m, parsed);
                        return;
                    }
                    break;
            }
            throw new UnhandledOpcodeException(parsed, ops);
        }

        private static void SetIp(MachineState m, Instruction instruct)
        {
            var next = (ushort)instruct.NextIP;
            m.IP = next;
        }
    }
}