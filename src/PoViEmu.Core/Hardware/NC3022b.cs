// ReSharper disable InconsistentNaming

using System.Linq;
using Iced.Intel;
using PoViEmu.Core.Decoding;
using PoViEmu.Core.Decoding.Ops;
using PoViEmu.Core.Hardware.Errors;
using U8 = PoViEmu.Core.Decoding.Ops.Consts.U8Operand;
using U16 = PoViEmu.Core.Decoding.Ops.Consts.U16Operand;
using I16 = PoViEmu.Core.Decoding.Ops.Consts.I16Operand;
using R8 = PoViEmu.Core.Decoding.Ops.Regs.Reg8Operand;
using R16 = PoViEmu.Core.Decoding.Ops.Regs.Reg16Operand;
using MO = PoViEmu.Core.Decoding.Ops.MemOperand;

namespace PoViEmu.Core.Hardware
{
    public sealed class NC3022
    {
        public void Execute(XInstruction instruct, MachineState m)
        {
            var parsed = instruct.Parsed;
            if (parsed.IsInvalidFor16Bit())
            {
                // TODO throw new InvalidOpcodeException(instruct);
                return;
            }
            var ops = parsed.GetOps().ToArray();
            switch (parsed.Mnemonic)
            {
                case Mnemonic.Mov:
                    if (ops is [R16, U16])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, R16])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, U16])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, R8])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, R16])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, R8])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, MO])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, MO])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Push:
                    if (ops is [R16])
                    {
                        return; // TODO
                    }
                    if (ops is [MO])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Xchg:
                    if (ops is [R16, R16])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, R8])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, R16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Lds:
                    if (ops is [R16, MO])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Cmpsb:
                    if (ops is [MO, MO])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Movsb:
                    if (ops is [MO, MO])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Scasb:
                    if (ops is [R8, MO])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Lodsb:
                    if (ops is [R8, MO])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Stosb:
                    if (ops is [MO, R8])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Pop:
                    if (ops is [R16])
                    {
                        return; // TODO
                    }
                    if (ops is [MO])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Popf:
                    if (ops.Length == 0)
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Ret:
                    if (ops.Length == 0)
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Hlt:
                    if (ops.Length == 0)
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Pushf:
                    if (ops.Length == 0)
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Cwd:
                    if (ops.Length == 0)
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Cbw:
                    if (ops.Length == 0)
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Int:
                    if (ops is [U8])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Jmp:
                    if (ops is [R16])
                    {
                        return; // TODO
                    }
                    if (ops is [U16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Jne:
                    if (ops is [U16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Ja:
                    if (ops is [U16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Jae:
                    if (ops is [U16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Jb:
                    if (ops is [U16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Jbe:
                    if (ops is [U16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Jg:
                    if (ops is [U16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Jno:
                    if (ops is [U16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Jnp:
                    if (ops is [U16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Jns:
                    if (ops is [U16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Jo:
                    if (ops is [U16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Jp:
                    if (ops is [U16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Js:
                    if (ops is [U16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Je:
                    if (ops is [U16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Jle:
                    if (ops is [U16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Jge:
                    if (ops is [U16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Jl:
                    if (ops is [U16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Adc:
                    if (ops is [R16, R16])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, U16])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, I16])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, MO])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, R16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Add:
                    if (ops is [MO, R8])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, R16])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, I16])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, U16])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, R16])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, MO])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, R8])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, I16])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, U16])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, MO])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Sbb:
                    if (ops is [R16, R16])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, MO])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, I16])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, R16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Sub:
                    if (ops is [R16, R16])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, I16])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, U16])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, MO])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, R8])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, R8])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, MO])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, R16])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, U16])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, I16])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, U8])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.And:
                    if (ops is [R16, R16])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, R8])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, U16])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, MO])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, MO])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, R8])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, R16])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, U16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Xor:
                    if (ops is [R16, R16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Rol:
                    if (ops is [R16, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, U8])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Aaa:
                    if (ops.Length == 0)
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Aas:
                    if (ops.Length == 0)
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Daa:
                    if (ops.Length == 0)
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Das:
                    if (ops.Length == 0)
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Clc:
                    if (ops.Length == 0)
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Cld:
                    if (ops.Length == 0)
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Cli:
                    if (ops.Length == 0)
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Nop:
                    if (ops.Length == 0)
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Cmc:
                    if (ops.Length == 0)
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Stc:
                    if (ops.Length == 0)
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Std:
                    if (ops.Length == 0)
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Sti:
                    if (ops.Length == 0)
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Rcr:
                    if (ops is [R16, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, R8])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, R8])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, R8])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Rcl:
                    if (ops is [R16, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, R8])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, R8])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, R8])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Les:
                    if (ops is [R16, MO])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Lea:
                    if (ops is [R16, MO])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Xlatb:
                    if (ops is [MO])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Or:
                    if (ops is [R16, R16])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, R8])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, MO])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, R8])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, U16])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, MO])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, R16])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, U16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Test:
                    if (ops is [R16, R16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Cmp:
                    if (ops is [R16, R16])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, U16])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, I16])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, MO])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, I16])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, U16])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, MO])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, R8])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, R16])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, R8])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Shl:
                    if (ops is [R16, R8])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, R8])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, R8])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Shr:
                    if (ops is [R16, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, U8])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Sahf:
                    if (ops.Length == 0)
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Lahf:
                    if (ops.Length == 0)
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Sar:
                    if (ops is [R16, R8])
                    {
                        return; // TODO
                    }
                    if (ops is [R16, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [R8, R8])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, U8])
                    {
                        return; // TODO
                    }
                    if (ops is [MO, R8])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Imul:
                    if (ops is [R16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Div:
                    if (ops is [R16])
                    {
                        return; // TODO
                    }
                    if (ops is [R8])
                    {
                        return; // TODO
                    }
                    if (ops is [MO])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Neg:
                    if (ops is [R16])
                    {
                        return; // TODO
                    }
                    if (ops is [R8])
                    {
                        return; // TODO
                    }
                    if (ops is [MO])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Loop:
                    if (ops is [U16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Call:
                    if (ops is [R16])
                    {
                        return; // TODO
                    }
                    if (ops is [MO])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Mul:
                    if (ops is [R16])
                    {
                        return; // TODO
                    }
                    if (ops is [R8])
                    {
                        return; // TODO
                    }
                    if (ops is [MO])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Not:
                    if (ops is [R16])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Idiv:
                    if (ops is [R16])
                    {
                        return; // TODO
                    }
                    if (ops is [MO])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Inc:
                    if (ops is [R16])
                    {
                        return; // TODO
                    }
                    if (ops is [MO])
                    {
                        return; // TODO
                    }
                    break;
                case Mnemonic.Dec:
                    if (ops is [R16])
                    {
                        return; // TODO
                    }
                    if (ops is [MO])
                    {
                        return; // TODO
                    }
                    break;
            }
            throw new UnhandledOpcodeException(parsed, ops);
        }
    }
}