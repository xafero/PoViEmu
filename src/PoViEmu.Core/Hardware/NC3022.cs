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
                case Mnemonic.Adc:
                    if (i is { OpCount: 2 })
                    {
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Immediate16 })
                        {
                            // TODO
                            return;
                        }
                    }
                    break;
                case Mnemonic.Or:
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
                case Mnemonic.Shl:
                    if (i is { OpCount: 2 })
                    {
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Register or OK.Immediate8 })
                        {
                            // TODO
                            return;
                        }
                    }
                    break;
                case Mnemonic.Sar:
                    if (i is { OpCount: 2 })
                    {
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Register })
                        {
                            // TODO
                            return;
                        }
                    }
                    break;
                case Mnemonic.Not:
                    if (i is { OpCount: 1 })
                    {
                        if (i is { Op0Kind: OK.Register })
                        {
                            // TODO
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
                            // TODO
                            return;
                        }
                        if (i is { Op0Kind: OK.Memory, Op1Kind: OK.Register })
                        {
                            // TODO
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
                            // TODO
                            return;
                        }
                        if (i is { Op0Kind: OK.Memory, Op1Kind: OK.Immediate8to16 })
                        {
                            // TODO
                            return;
                        }
                        if (i is { Op0Kind: OK.Register, Op1Kind: OK.Immediate16 or OK.Immediate8to16 })
                        {
                            // TODO
                            return;
                        }
                    }
                    break;
                case Mnemonic.Retf:
                    if (i is { OpCount: 0 })
                    {
                        // TODO
                        return;
                    }
                    break;
                case Mnemonic.Cwd:
                    if (i is { OpCount: 0 })
                    {
                        // TODO
                        return;
                    }
                    break;
                case Mnemonic.Cbw:
                    if (i is { OpCount: 0 })
                    {
                        // TODO
                        return;
                    }
                    break;
                case Mnemonic.Mul:
                    if (i is { OpCount: 1 })
                    {
                        /* TODO */
                        return;
                    }
                    break;
                case Mnemonic.Idiv:
                    if (i is { OpCount: 1 })
                    {
                        /* TODO */
                        return;
                    }
                    break;
                case Mnemonic.Jne:
                    if (i is { OpCount: 1 })
                    {
                        /* TODO */
                        return;
                    }
                    break;
                case Mnemonic.Je:
                    if (i is { OpCount: 1 })
                    {
                        /* TODO */
                        return;
                    }
                    break;
                case Mnemonic.Jl:
                    if (i is { OpCount: 1 })
                    {
                        /* TODO */
                        return;
                    }
                    break;
                case Mnemonic.Jle:
                    if (i is { OpCount: 1 })
                    {
                        /* TODO */
                        return;
                    }
                    break;
                case Mnemonic.Jge:
                    if (i is { OpCount: 1 })
                    {
                        /* TODO */
                        return;
                    }
                    break;
                case Mnemonic.Jmp:
                    if (i is { OpCount: 1 })
                    {
                        /* TODO */
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