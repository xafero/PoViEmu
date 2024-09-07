using System;
using Iced.Intel;
using PoViEmu.Core.Decoding;
using OK = Iced.Intel.OpKind;

// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Hardware
{
    public sealed class NC3022
    {
        public void Execute(ref MachineState s, Instruction i)
        {
            var m = i.Mnemonic;
            switch (m)
            {
                case Mnemonic.Push:
                    if (i is { OpCount: 1, Op0Kind: OK.Register })
                    {
                        var newTop = s.Get(i.Op0Register);
                        s.Push(newTop);
                        return;
                    }
                    break;
                case Mnemonic.Pop:
                    if (i is { OpCount: 1, Op0Kind: OK.Register })
                    {
                        s.Pop(out var oldTop);
                        s.Set(i.Op0Register, oldTop);
                        return;
                    }
                    break;
                case Mnemonic.And:
                    if (i is { OpCount: 2 })
                    {
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Register })
                        {
                            var andRR = (ushort)(s.Get(i.Op0Register) & s.Get(i.Op1Register));
                            s.Set(i.Op0Register, andRR);
                            return;
                        }
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Memory })
                        {
                            var andRM = (ushort)(s.Get(i.Op0Register) & s.ReadMem(i, 2));
                            s.Set(i.Op0Register, andRM);
                            return;
                        }
                    }
                    break;
                case Mnemonic.Adc:
                    if (i is { OpCount: 2 })
                    {
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Immediate16 })
                        {
                            var adcRI = (ushort)(s.Get(i.Op0Register) + i.Immediate16 + (s.CF ? 1 : 0));
                            s.Set(i.Op0Register, adcRI);
                            return;
                        }
                    }
                    break;
                case Mnemonic.Or:
                    if (i is { OpCount: 2 })
                    {
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Register })
                        {
                            var orRR = (ushort)(s.Get(i.Op0Register) | s.Get(i.Op1Register));
                            s.Set(i.Op0Register, orRR);
                            return;
                        }
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Memory })
                        {
                            var orRM = (ushort)(s.Get(i.Op0Register) | s.ReadMem(i, 2));
                            s.Set(i.Op0Register, orRM);
                            return;
                        }
                    }
                    break;
                case Mnemonic.Shl:
                    if (i is { OpCount: 2 })
                    {
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Register or OK.Immediate8 })
                        {
                            var shift = i.Op1Kind == OK.Register ? s.Get(i.Op1Register) : i.Immediate8;
                            var shlRR = (ushort)(s.Get(i.Op0Register) << shift);
                            s.Set(i.Op0Register, shlRR);
                            return;
                        }
                    }
                    break;
                case Mnemonic.Sar:
                    if (i is { OpCount: 2 })
                    {
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Register })
                        {
                            var shift = s.Get(i.Op1Register);
                            var sarRR = (ushort)(s.Get(i.Op0Register) >> shift);
                            s.Set(i.Op0Register, sarRR);
                            return;
                        }
                    }
                    break;
                case Mnemonic.Not:
                    if (i is { OpCount: 1 })
                    {
                        if (i is { Op0Kind: OK.Register })
                        {
                            var notR = (ushort)~s.Get(i.Op0Register);
                            s.Set(i.Op0Register, notR);
                            return;
                        }
                    }
                    break;
                case Mnemonic.Add:
                    if (i is { OpCount: 2 })
                    {
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Register or OK.Immediate16 })
                        {
                            var plus = (ushort)(s.Get(i.Op0Register) + s.GetValue(i, 1));
                            s.Set(i.Op0Register, plus);
                            return;
                        }
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Memory })
                        {
                            var plus = (ushort)(s.Get(i.Op0Register) + s.ReadMem(i, 2));
                            s.Set(i.Op0Register, plus);
                            return;
                        }
                        if (i is { Op0Kind: OK.Memory, Op1Kind: OK.Register })
                        {
                            var plus = (ushort)(s.ReadMem(i, 2) + s.Get(i.Op1Register));
                            s.WriteMem(i, 2, plus);
                            return;
                        }
                    }
                    break;
                case Mnemonic.Sub:
                    if (i is { OpCount: 2 })
                    {
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Register })
                        {
                            var minus = (ushort)(s.Get(i.Op0Register) - s.Get(i.Op1Register));
                            s.Set(i.Op0Register, minus);
                            return;
                        }
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Memory })
                        {
                            var minus = (ushort)(s.Get(i.Op0Register) - s.ReadMem(i, 2));
                            s.Set(i.Op0Register, minus);
                            return;
                        }
                        if (i is { Op0Kind: OK.Memory, Op1Kind: OK.Immediate8to16 })
                        {
                            var minus = (ushort)(s.ReadMem(i, 2) - i.Immediate8to16);
                            s.WriteMem(i, 2, minus);
                            return;
                        }
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Immediate16 or OK.Immediate8to16 })
                        {
                            var minus = (ushort)(s.Get(i.Op0Register) - s.GetValue(i, 1));
                            s.Set(i.Op0Register, minus);
                            return;
                        }
                    }
                    break;
                case Mnemonic.Retf:
                    if (i is { OpCount: 0 })
                    {
                        s.Pop(out var ip);
                        s.Pop(out var cs);
                        s.IP = ip;
                        s.CS = cs;
                        return;
                    }
                    break;
                case Mnemonic.Cwd:
                    if (i is { OpCount: 0 })
                    {
                        s.DX = (ushort)(s.AX >= 0x8000 ? 0xFFFF : 0x0000);
                        return;
                    }
                    break;
                case Mnemonic.Cbw:
                    if (i is { OpCount: 0 })
                    {
                        s.AX = (ushort)((s.AL() & 0x80) != 0 ? (s.AX | 0xFF00) : (s.AX & 0x00FF));
                        return;
                    }
                    break;
                case Mnemonic.Mul:
                    if (i is { OpCount: 1 })
                    {
                        var mulResult = (uint)(s.AX * s.Get(i.Op0Register));
                        s.DX = (ushort)(mulResult >> 16);
                        s.AX = (ushort)(mulResult & 0xFFFF);
                        return;
                    }
                    break;
                case Mnemonic.Idiv:
                    if (i is { OpCount: 1 })
                    {
                        var dividend = ((uint)s.DX << 16) | s.AX;
                        var divisor = s.Get(i.Op0Register);
                        var quotient = dividend / divisor;
                        var remainder = dividend % divisor;
                        s.AX = (ushort)(quotient & 0xFFFF);
                        s.DX = (ushort)(remainder & 0xFFFF);
                        return;
                    }
                    break;
                case Mnemonic.Jne:
                    if (i is { OpCount: 1 })
                    {
                        // Jump if not equal
                        if (!s.ZF) s.IP = i.NearBranch16;
                        return;
                    }
                    break;
                case Mnemonic.Je:
                    if (i is { OpCount: 1 })
                    {
                        // Jump if equal
                        if (s.ZF) s.IP = i.NearBranch16;
                        return;
                    }
                    break;
                case Mnemonic.Jl:
                    if (i is { OpCount: 1 })
                    {
                        // Jump if less
                        if (s.SF != s.OF) s.IP = i.NearBranch16;
                        return;
                    }
                    break;
                case Mnemonic.Jle:
                    if (i is { OpCount: 1 })
                    {
                        // Jump if less or equal
                        if (s.ZF || (s.SF != s.OF)) s.IP = i.NearBranch16;
                        return;
                    }
                    break;
                case Mnemonic.Jge:
                    if (i is { OpCount: 1 })
                    {
                        // Jump if greater or equal
                        if (s.SF == s.OF) s.IP = i.NearBranch16;
                        return;
                    }
                    break;
                case Mnemonic.Jmp:
                    if (i is { OpCount: 1 })
                    {
                        // Unconditional jump
                        s.IP = i.NearBranch16;
                        return;
                    }
                    break;
                case Mnemonic.Dec:
                    if (i is { OpCount: 1 })
                    {
                        if (i is { Op0Kind: OK.Register })
                        {
                            var dec = (ushort)(s.Get(i.Op0Register) - 1);
                            s.Set(i.Op0Register, dec);
                            return;
                        }
                        if (i is { Op0Kind: OK.Memory })
                        {
                            // TODO
                            return;
                        }
                    }
                    break;
                case Mnemonic.Inc:
                    if (i is { OpCount: 1 })
                    {
                        if (i is { Op0Kind: OK.Register })
                        {
                            var inc = (ushort)(s.Get(i.Op0Register) + 1);
                            s.Set(i.Op0Register, inc);
                            return;
                        }
                        if (i is { Op0Kind: OK.Memory })
                        {
                            // TODO
                            return;
                        }
                    }
                    break;
                case Mnemonic.Test:
                    if (i is { OpCount: 2 })
                    {
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Register })
                        {
                            // TODO
                            return;
                        }
                    }
                    break;
                case Mnemonic.Cmp:
                    if (i is { OpCount: 2 })
                    {
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Register })
                        {
                            // TODO (OF, SF, ZF, AF, PF, CF)
                            var diff = s.Get(i.Op0Register) - s.Get(i.Op1Register);
                            s.ZF = diff == 0;
                            return;
                        }
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Memory })
                        {
                            // TODO
                            return;
                        }
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Immediate16 or OK.Immediate8to16 })
                        {
                            // TODO
                            return;
                        }
                        if (i is { Op0Kind: OK.Memory, Op1Kind: OK.Immediate16 or OK.Immediate8to16 })
                        {
                            // TODO
                            return;
                        }
                    }
                    break;
                case Mnemonic.Xor:
                    if (i is { OpCount: 2 })
                    {
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Register })
                        {
                            // TODO
                            return;
                        }
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Memory })
                        {
                            // TODO
                            return;
                        }
                    }
                    break;
                case Mnemonic.Mov:
                    if (i is { OpCount: 2 })
                    {
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Immediate16 or OK.Register })
                        {
                            s.Set(i.Op0Register, s.GetValue(i, 1));
                            return;
                        }
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Memory })
                        {
                            // TODO
                            return;
                        }
                        if (i is { Op0Kind: OK.Memory, Op1Kind: OK.Register })
                        {
                            // TODO
                            return;
                        }
                        if (i is { Op0Kind: OK.Memory, Op1Kind: OK.Immediate16 })
                        {
                            // TODO
                            return;
                        }
                    }
                    break;
            }
            throw new ArgumentException($"{m} ({i}) {{{i.GetOpDebug()}}} ?!");
        }
    }
}