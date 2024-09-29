using System;
using System.Linq;
using Iced.Intel;
using PoViEmu.Core.Hardware;
using OK = Iced.Intel.OpKind;
using R = Iced.Intel.Register;

#pragma warning disable CS8509

namespace PoViEmu.Core.Decoding
{
    public static class StateExt
    {
        public const int StackSize = 2;

        public static void Push(this MachineState state, ushort value)
        {
            state.SP -= StackSize;

            // var list = state.Stack.First();
            // list.Value.Insert(0, value);
        }

        public static void Pop(this MachineState state, out ushort value)
        {
            // var list = state.Stack.First();
            // value = list.Value.First();
            // list.Value.RemoveAt(0);
            value = default;

            state.SP += StackSize;
        }

        public static ushort GetValue(this MachineState state, Instruction instr, int opNr)
        {
            return opNr switch
            {
                1 => instr.Op1Kind switch
                {
                    OK.Immediate16 => instr.Immediate16,
                    // OK.Register => state.Get(instr.Op1Register),
                },
                _ => throw new InvalidOperationException($"{instr} / {opNr}")
            };
        }

        public static byte ReadMem(this MachineState state, Instruction i, int size)
        {
            throw new InvalidOperationException("?! " + i + " " + size);
        }

        public static byte WriteMem(this MachineState state, Instruction i, int size, ushort value)
        {
            throw new InvalidOperationException("?! " + i + " " + size + " " + value);
        }

        public static ushort Get(this MachineState state, R register)
        {
            return register switch
            {
                R.AX => state.AX,
                // R.AL => state.AL(),
                // TODO R.AH => state.AH(),
                R.BX => state.BX,
                // TODO R.BL => state.BL(),
                // TODO R.BH => state.BH(),
                R.CX => state.CX,
                // TODO R.CL => state.CL(),
                // TODO R.CH => state.CH(),
                R.DX => state.DX,
                // TODO R.DL => state.DL(),
                // TODO R.DH => state.DH(),
                R.SP => state.SP,
                R.BP => state.BP,
                R.SI => state.SI,
                R.DI => state.DI,
                R.ES => state.ES,
                R.CS => state.CS,
                R.SS => state.SS,
                R.DS => state.DS,
                _ => throw new InvalidOperationException($"{register}")
            };
        }

        public static void Set(this MachineState state, R register, ushort value)
        {
            switch (register)
            {
                case R.AX:
                    state.AX = value;
                    break;
                case R.BX:
                    state.BX = value;
                    break;
                case R.CX:
                    state.CX = value;
                    break;
                case R.DX:
                    state.DX = value;
                    break;
                case R.SP:
                    state.SP = value;
                    break;
                case R.BP:
                    state.BP = value;
                    break;
                case R.SI:
                    state.SI = value;
                    break;
                case R.DI:
                    state.DI = value;
                    break;
                case R.ES:
                    state.ES = value;
                    break;
                case R.CS:
                    state.CS = value;
                    break;
                case R.SS:
                    state.SS = value;
                    break;
                case R.DS:
                    state.DS = value;
                    break;
                default: throw new InvalidOperationException($"{register}");
            }
        }

        public static string? ToTxt(this R register)
            => register == default ? null : register.ToString();

        public static string GetOpDebug(this Instruction i, bool fake = false)
            => string.Join(" ; ", Enumerable.Range(0, i.OpCount)
                .Select(n =>
                {
                    var kind = i.GetOpKind(n);
                    var val = string.Empty;
                    switch (kind)
                    {
                        case OK.Register:
                            var rs = i.GetOpRegister(n);
                            val = $"R_{(fake ? "!" : rs)}";
                            break;
                        case OK.Memory:
                            val = "M";
                            break;
                        case OK.Immediate8:
                        case OK.Immediate8to16:
                            var i8S = i.GetImmediate(n);
                            val = $"{(fake ? 0x08 : i8S):x2}";
                            break;
                        case OK.Immediate16:
                            var i16S = i.GetImmediate(n);
                            val = $"{(fake ? 0x16 : i16S):x4}";
                            break;
                    }
                    return val;
                })
            );

        public static byte AL(this MachineState state)
        {
            throw new NotImplementedException("?");
        }
    }
}