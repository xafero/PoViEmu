// ReSharper disable InconsistentNaming

using System.Linq;
using Iced.Intel;
using PoViEmu.Core.Decoding;
using PoViEmu.Core.Decoding.Ops;
using PoViEmu.Core.Decoding.Ops.Consts;
using PoViEmu.Core.Decoding.Ops.Regs;
using PoViEmu.Core.Hardware.AckNow;
using PoViEmu.Core.Hardware.Errors;

namespace PoViEmu.Core.Hardware
{
    public sealed class NC3022
    {
        public void Execute(XInstruction instruct, MachineState m)
        {
            var parsed = instruct.Parsed;
            if (parsed.IsInvalidFor16Bit())
            {
                throw new InvalidOpcodeException(instruct);
            }
            var ops = parsed.GetOps().ToArray();
            switch (parsed.Mnemonic)
            {
                case Mnemonic.Mov:
                    if (ops is [Reg16Operand movR2, Reg16Operand movR3])
                    {
                        var movV = m[movR3];
                        m[movR2] = movV;
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [Reg16Operand movR1, U16Operand movI1])
                    {
                        var movV = movI1.Val;
                        m[movR1] = movV;
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [Reg16Operand movR4, MemOperand movM2])
                    {
                        var movT = m[movR4];
                        // TODO Read memory?
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [Reg8Operand movR6, MemOperand movM4])
                    {
                        var movT = m[movR6];
                        // TODO Read memory?
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [MemOperand movM1, U16Operand movI2])
                    {
                        var movV = movI2.Val;
                        // TODO Write memory?
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [MemOperand movM3, Reg16Operand movR5])
                    {
                        var movV = m[movR5];
                        // TODO Write memory?
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Sub:
                    if (ops is [Reg16Operand subR1, I16Operand subI1])
                    {
                        var subT = m[subR1];
                        var subV = subI1.Val;
                        m[subR1] = (ushort)(subT - subV);
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [Reg16Operand subR2, Reg16Operand subR3])
                    {
                        var subT = m[subR2];
                        var subV = m[subR3];
                        m[subR2] = (ushort)(subT - subV);
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [Reg16Operand subR4, MemOperand subM1])
                    {
                        var subT = m[subR4];
                        // TODO
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Add:
                    if (ops is [Reg16Operand addR1, Reg16Operand addR2])
                    {
                        var addT = m[addR1];
                        var addV = m[addR2];
                        m[addR1] = (ushort)(addT + addV);
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [Reg16Operand addR3, U16Operand addU1])
                    {
                        var addT = m[addR3];
                        var addV = addU1.Val;
                        m[addR3] = (ushort)(addT + addV);
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [Reg16Operand addR4, MemOperand addM1])
                    {
                        var addT = m[addR4];
                        // TODO
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Xor:
                    if (ops is [Reg16Operand xorR1, Reg16Operand xorR2])
                    {
                        var xorT = m[xorR1];
                        var xorV = m[xorR2];
                        m[xorR1] = (ushort)(xorT ^ xorV);
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Or:
                    if (ops is [Reg16Operand orR1, Reg16Operand orR2])
                    {
                        var orT = m[orR1];
                        var orV = m[orR2];
                        m[orR1] = (ushort)(orT | orV);
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.And:
                    if (ops is [Reg16Operand andR1, Reg16Operand andR2])
                    {
                        var andT = m[andR1];
                        var andV = m[andR2];
                        m[andR1] = (ushort)(andT & andV);
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [Reg16Operand andR3, MemOperand andM1])
                    {
                        var andT = m[andR3];
                        // TODO
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Shl:
                    if (ops is [Reg16Operand shlR1, Reg16Operand shlR2])
                    {
                        var shlT = m[shlR1];
                        var shlV = m[shlR2];
                        m[shlR1] = (ushort)(shlT << shlV);
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [Reg16Operand shlR3, Reg8Operand shlR4])
                    {
                        var shlT = m[shlR3];
                        var shlV = m[shlR4];
                        m[shlR3] = (ushort)(shlT << shlV);
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Sar:
                    if (ops is [Reg16Operand sarR1, Reg16Operand sarR2])
                    {
                        var sarT = m[sarR1];
                        var sarV = m[sarR2];
                        m[sarR1] = (ushort)(sarT >> sarV);
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [Reg16Operand sarR3, Reg8Operand sarR4])
                    {
                        var sarT = m[sarR3];
                        var sarV = m[sarR4];
                        m[sarR3] = (ushort)(sarT >> sarV);
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Cmp:
                    if (ops is [Reg16Operand cmpR1, Reg16Operand cmpR2])
                    {
                        var cmpO1 = m[cmpR1];
                        var cmpO2 = m[cmpR2];
                        var cmpR = cmpO1 - cmpO2;
                        m.ZF = cmpR == 0;
                        m.SF = cmpR < 0;
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [Reg16Operand cmpR3, U16Operand cmpU1])
                    {
                        var cmpO1 = m[cmpR3];
                        var cmpO2 = cmpU1.Val;
                        var cmpR = cmpO1 - cmpO2;
                        m.ZF = cmpR == 0;
                        m.SF = cmpR < 0;
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [Reg16Operand cmpR4, MemOperand cmpM1])
                    {
                        var cmpO1 = m[cmpR4];
                        // TODO
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [MemOperand cmpM2, I16Operand cmpI1])
                    {
                        // TODO
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Test:
                    if (ops is [Reg16Operand testR1, Reg16Operand testR2])
                    {
                        var testO1 = m[testR1];
                        var testO2 = m[testR2];
                        var testR = testO1 & testO2;
                        m.ZF = testR == 0;
                        m.SF = testR < 0;
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Jne:
                    if (ops is [U16Operand jneU1])
                    {
                        if (!m.ZF)
                        {
                            SetIp(m, parsed, jneU1);
                            return;
                        }
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Je:
                    if (ops is [U16Operand jeU1])
                    {
                        if (m.ZF)
                        {
                            SetIp(m, parsed, jeU1);
                            return;
                        }
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Jle:
                    if (ops is [U16Operand jleU1])
                    {
                        if (m.ZF)
                        {
                            SetIp(m, parsed, jleU1);
                            return;
                        }
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Jge:
                    if (ops is [U16Operand jgeU1])
                    {
                        if (m.ZF)
                        {
                            SetIp(m, parsed, jgeU1);
                            return;
                        }
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Jl:
                    if (ops is [U16Operand jlU1])
                    {
                        if (m.ZF)
                        {
                            SetIp(m, parsed, jlU1);
                            return;
                        }
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Jmp:
                    if (ops is [U16Operand jmpU1])
                    {
                        SetIp(m, parsed, jmpU1);
                        return;
                    }
                    break;
                case Mnemonic.Dec:
                    if (ops is [Reg16Operand decR1])
                    {
                        var decT = m[decR1];
                        var decN = decT - 1;
                        m[decR1] = (ushort)decN;
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [MemOperand decM1])
                    {
                        // TODO
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Inc:
                    if (ops is [Reg16Operand incR1])
                    {
                        var incT = m[incR1];
                        var incN = incT + 1;
                        m[incR1] = (ushort)incN;
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [MemOperand incM1])
                    {
                        // TODO
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Not:
                    if (ops is [Reg16Operand notR1])
                    {
                        var notT = m[notR1];
                        var notN = ~notT;
                        m[notR1] = (ushort)notN;
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Mul:
                    if (ops is [Reg16Operand mulR1])
                    {
                        var mulAx = m[B16Register.AX];
                        var mulF = m[mulR1];
                        var mulV = mulAx * mulF;
                        var (mulL, mulH) = mulV.SplitInt();
                        m[B16Register.AX] = mulL;
                        m[B16Register.DX] = mulH;
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [MemOperand mulM1])
                    {
                        // TODO
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Idiv:
                    if (ops is [Reg16Operand idivR1])
                    {
                        var idivDx = m[B16Register.DX];
                        var idivAx = m[B16Register.AX];
                        var idivS = (idivAx, idivDx).CombineInt();
                        var idivF = m[idivR1];
                        m[B16Register.AX] = (ushort)(idivS / idivF);
                        m[B16Register.DX] = (ushort)(idivS % idivF);
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [MemOperand idivM1])
                    {
                        // TODO
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Cwd:
                    var cwdAx = (short)m[B16Register.AX];
                    m[B16Register.DX] = (ushort)(cwdAx >= 0 ? 0x0000 : 0xFFFF);
                    SetIp(m, parsed);
                    return;
                case Mnemonic.Cbw:
                    // TODO
                    SetIp(m, parsed);
                    return;
                case Mnemonic.Push:
                    if (ops is [Reg16Operand pushR])
                    {
                        var pushS = m[pushR];
                        m.TopOfStack = pushS;
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Pop:
                    if (ops is [Reg16Operand popR])
                    {
                        var popS = m.TopOfStack;
                        m[popR] = popS;
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

        private static void SetIp(MachineState m, Instruction instruct, U16Operand jump)
        {
            var next = (ushort)instruct.NextIP;
            next = (ushort)(next + jump.Val);
            m.IP = next;
        }
    }
}