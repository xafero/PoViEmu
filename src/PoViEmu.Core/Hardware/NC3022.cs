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
            if (parsed.IsInvalidFor16Bit())
            {
                throw new InvalidOpcodeException(instruct);
            }
            var ops = parsed.GetOps().ToArray();
            switch (parsed.Mnemonic)
            {
                case Mnemonic.Mov:
                    if (ops is [RegOperand movR1, U16Operand movI1])
                    {
                        var movT = m.GetRef(movR1);
                        var movV = movI1.Val;
                        movT.Set(movV);
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [RegOperand movR4, MemOperand movM2])
                    {
                        var movT = m.GetRef(movR4);
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
                    if (ops is [MemOperand movM3, RegOperand movR5])
                    {
                        var movV = m.GetRef(movR5);
                        // TODO Write memory?
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [RegOperand movR2, RegOperand movR3])
                    {
                        var movT = m.GetRef(movR2);
                        var movV = m.GetRef(movR3);
                        movT.Set(movV.U16());
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Sub:
                    if (ops is [RegOperand subR1, I16Operand subI1])
                    {
                        var subT = m.GetRef(subR1);
                        var subV = subI1.Val;
                        subT.Set(subT.U16() - subV);
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [RegOperand subR2, RegOperand subR3])
                    {
                        var subT = m.GetRef(subR2);
                        var subV = m.GetRef(subR3);
                        subT.Set(subT.U16() - subV.U16());
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [RegOperand subR4, MemOperand subM1])
                    {
                        var subT = m.GetRef(subR4);
                        // TODO
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Add:
                    if (ops is [RegOperand addR1, RegOperand addR2])
                    {
                        var addT = m.GetRef(addR1);
                        var addV = m.GetRef(addR2);
                        addT.Set(addT.U16() + addV.U16());
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [RegOperand addR3, U16Operand addU1])
                    {
                        var addT = m.GetRef(addR3);
                        var addV = addU1.Val;
                        addT.Set(addT.U16() + addV);
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [RegOperand addR4, MemOperand addM1])
                    {
                        var addT = m.GetRef(addR4);
                        // TODO
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Xor:
                    if (ops is [RegOperand xorR1, RegOperand xorR2])
                    {
                        var xorT = m.GetRef(xorR1);
                        var xorV = m.GetRef(xorR2);
                        xorT.Set(xorT.U16() ^ xorV.U16());
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Or:
                    if (ops is [RegOperand orR1, RegOperand orR2])
                    {
                        var orT = m.GetRef(orR1);
                        var orV = m.GetRef(orR2);
                        orT.Set(orT.U16() | orV.U16());
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.And:
                    if (ops is [RegOperand andR1, RegOperand andR2])
                    {
                        var andT = m.GetRef(andR1);
                        var andV = m.GetRef(andR2);
                        andT.Set(andT.U16() & andV.U16());
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [RegOperand andR3, MemOperand andM1])
                    {
                        var andT = m.GetRef(andR3);
                        // TODO
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Shl:
                    if (ops is [RegOperand shlR1, RegOperand shlR2])
                    {
                        var shlT = m.GetRef(shlR1);
                        var shlV = m.GetRef(shlR2);
                        shlT.Set(shlT.U16() << shlV.U16());
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Sar:
                    if (ops is [RegOperand sarR1, RegOperand sarR2])
                    {
                        var sarT = m.GetRef(sarR1);
                        var sarV = m.GetRef(sarR2);
                        sarT.Set(sarT.U16() >> sarV.U16());
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Cmp:
                    if (ops is [RegOperand cmpR1, RegOperand cmpR2])
                    {
                        var cmpO1 = m.GetRef(cmpR1);
                        var cmpO2 = m.GetRef(cmpR2);
                        var cmpR = cmpO1.U16() - cmpO2.U16();
                        m.ZF = cmpR == 0;
                        m.SF = cmpR < 0;
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [RegOperand cmpR3, U16Operand cmpU1])
                    {
                        var cmpO1 = m.GetRef(cmpR3);
                        var cmpO2 = cmpU1.Val;
                        var cmpR = cmpO1.U16() - cmpO2;
                        m.ZF = cmpR == 0;
                        m.SF = cmpR < 0;
                        SetIp(m, parsed);
                        return;
                    }
                    if (ops is [RegOperand cmpR4, MemOperand cmpM1])
                    {
                        var cmpO1 = m.GetRef(cmpR4);
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
                    if (ops is [RegOperand testR1, RegOperand testR2])
                    {
                        var testO1 = m.GetRef(testR1);
                        var testO2 = m.GetRef(testR2);
                        var testR = testO1.U16() & testO2.U16();
                        m.ZF = testR == 0;
                        m.SF = testR < 0;
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Jne:
                    if (ops is [BrU16Operand jneU1])
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
                    if (ops is [BrU16Operand jeU1])
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
                    if (ops is [BrU16Operand jleU1])
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
                    if (ops is [BrU16Operand jgeU1])
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
                    if (ops is [BrU16Operand jlU1])
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
                    if (ops is [BrU16Operand jmpU1])
                    {
                        SetIp(m, parsed, jmpU1);
                        return;
                    }
                    break;
                case Mnemonic.Dec:
                    if (ops is [RegOperand decR1])
                    {
                        var decT = m.GetRef(decR1);
                        var decN = decT.U16() - 1;
                        decT.Set(decN);
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
                    if (ops is [RegOperand incR1])
                    {
                        var incT = m.GetRef(incR1);
                        var incN = incT.U16() + 1;
                        incT.Set(incN);
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
                    if (ops is [RegOperand notR1])
                    {
                        var notT = m.GetRef(notR1);
                        var notN = ~notT.U16();
                        notT.Set(notN);
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Mul:
                    if (ops is [RegOperand mulR1])
                    {
                        var mulAx = m.GetRef(Register.AX);
                        var mulF = m.GetRef(mulR1);
                        var mulV = mulAx.U16() * mulF.U16();
                        var (mulL, mulH) = MachTool.SplitInt(mulV);
                        var mulDx = m.GetRef(Register.DX);
                        mulAx.Set(mulL);
                        mulDx.Set(mulH);
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
                    if (ops is [RegOperand idivR1])
                    {
                        var idivDx = m.GetRef(Register.DX);
                        var idivAx = m.GetRef(Register.AX);
                        var idivS = MachTool.CombineInt(idivAx.U16(), idivDx.U16());
                        var idivF = m.GetRef(idivR1);
                        idivAx.Set(idivS / idivF.U16());
                        idivDx.Set(idivS % idivF.U16());
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
                    var cwdAx = m.GetRef(Register.AX);
                    var cwdDx = m.GetRef(Register.DX);
                    cwdDx.Set(cwdAx.I32() >= 0 ? 0x0000 : 0xFFFF);
                    SetIp(m, parsed);
                    return;
                case Mnemonic.Cbw:
                    // TODO
                    SetIp(m, parsed);
                    return;
                case Mnemonic.Push:
                    if (ops is [RegOperand pushR])
                    {
                        var pushS = m.GetRef(pushR);
                        m.Push(pushS.U16());
                        SetIp(m, parsed);
                        return;
                    }
                    break;
                case Mnemonic.Pop:
                    if (ops is [RegOperand popR])
                    {
                        var popT = m.GetRef(popR);
                        popT.Set(m.Pop());
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

        private static void SetIp(MachineState m, Instruction instruct, BrU16Operand jump)
        {
            var next = (ushort)instruct.NextIP;
            next = (ushort)(next + jump.Val);
            m.IP = next;
        }
    }
}