using System;
using PoViEmu.SH3.ISA;
using static PoViEmu.Base.PropHelper;

namespace PoViEmu.SH3.CPU
{
    public static class MachParser
    {
        public static object? GetByString(this MachineState m, string? name)
        {
            var (key, arg) = SplitPropName(name);
            return key switch
            {
                nameof(MachineState.R0) => m.R0,
                nameof(MachineState.R1) => m.R1,
                nameof(MachineState.R2) => m.R2,
                nameof(MachineState.R3) => m.R3,
                nameof(MachineState.R4) => m.R4,
                nameof(MachineState.R5) => m.R5,
                nameof(MachineState.R6) => m.R6,
                nameof(MachineState.R7) => m.R7,
                nameof(MachineState.R8) => m.R8,
                nameof(MachineState.R9) => m.R9,
                nameof(MachineState.R10) => m.R10,
                nameof(MachineState.R11) => m.R11,
                nameof(MachineState.R12) => m.R12,
                nameof(MachineState.R13) => m.R13,
                nameof(MachineState.R14) => m.R14,
                nameof(MachineState.R15) => m.R15,
                nameof(MachineState.R0_b) => m.R0_b,
                nameof(MachineState.R1_b) => m.R1_b,
                nameof(MachineState.R2_b) => m.R2_b,
                nameof(MachineState.R3_b) => m.R3_b,
                nameof(MachineState.R4_b) => m.R4_b,
                nameof(MachineState.R5_b) => m.R5_b,
                nameof(MachineState.R6_b) => m.R6_b,
                nameof(MachineState.R7_b) => m.R7_b,
                nameof(MachineState.PC) => m.PC,
                nameof(MachineState.dPC) => m.dPC,
                nameof(MachineState.PR) => m.PR,
                nameof(MachineState.MACH) => m.MACH,
                nameof(MachineState.MACL) => m.MACL,
                nameof(MachineState.GBR) => m.GBR,
                nameof(MachineState.VBR) => m.VBR,
                nameof(MachineState.SPC) => m.SPC,
                nameof(MachineState.SSR) => m.SSR,
                nameof(MachineState.T) => m.T,
                nameof(MachineState.S) => m.S,
                nameof(MachineState.Q) => m.Q,
                nameof(MachineState.M) => m.M,
                nameof(MachineState.I0) => m.I0,
                nameof(MachineState.I1) => m.I1,
                nameof(MachineState.I2) => m.I2,
                nameof(MachineState.I3) => m.I3,
                nameof(MachineState.U8) => m.GetU8(arg),
                nameof(MachineState.U8A) => m.GetU8A(arg),
                nameof(MachineState.U16) => m.GetU16(arg),
                nameof(MachineState.U16A) => m.GetU16A(arg),
                nameof(MachineState.U32) => m.GetU32(arg),
                nameof(MachineState.U32A) => m.GetU32A(arg),
                _ => throw new InvalidOperationException(name)
            };
        }

        public static void SetByString(this MachineState m, string name, object value)
        {
            var (key, arg) = SplitPropName(name);
            switch (key)
            {
                case nameof(MachineState.R0): m.R0 = ToVal<uint>(value); break;
                case nameof(MachineState.R1): m.R1 = ToVal<uint>(value); break;
                case nameof(MachineState.R2): m.R2 = ToVal<uint>(value); break;
                case nameof(MachineState.R3): m.R3 = ToVal<uint>(value); break;
                case nameof(MachineState.R4): m.R4 = ToVal<uint>(value); break;
                case nameof(MachineState.R5): m.R5 = ToVal<uint>(value); break;
                case nameof(MachineState.R6): m.R6 = ToVal<uint>(value); break;
                case nameof(MachineState.R7): m.R7 = ToVal<uint>(value); break;
                case nameof(MachineState.R8): m.R8 = ToVal<uint>(value); break;
                case nameof(MachineState.R9): m.R9 = ToVal<uint>(value); break;
                case nameof(MachineState.R10): m.R10 = ToVal<uint>(value); break;
                case nameof(MachineState.R11): m.R11 = ToVal<uint>(value); break;
                case nameof(MachineState.R12): m.R12 = ToVal<uint>(value); break;
                case nameof(MachineState.R13): m.R13 = ToVal<uint>(value); break;
                case nameof(MachineState.R14): m.R14 = ToVal<uint>(value); break;
                case nameof(MachineState.R15): m.R15 = ToVal<uint>(value); break;
                case nameof(MachineState.R0_b): m.R0_b = ToVal<uint>(value); break;
                case nameof(MachineState.R1_b): m.R1_b = ToVal<uint>(value); break;
                case nameof(MachineState.R2_b): m.R2_b = ToVal<uint>(value); break;
                case nameof(MachineState.R3_b): m.R3_b = ToVal<uint>(value); break;
                case nameof(MachineState.R4_b): m.R4_b = ToVal<uint>(value); break;
                case nameof(MachineState.R5_b): m.R5_b = ToVal<uint>(value); break;
                case nameof(MachineState.R6_b): m.R6_b = ToVal<uint>(value); break;
                case nameof(MachineState.R7_b): m.R7_b = ToVal<uint>(value); break;
                case nameof(MachineState.MACH): m.MACH = ToVal<uint>(value); break;
                case nameof(MachineState.MACL): m.MACL = ToVal<uint>(value); break;
                case nameof(MachineState.GBR): m.GBR = ToVal<uint>(value); break;
                case nameof(MachineState.VBR): m.VBR = ToVal<uint>(value); break;
                case nameof(MachineState.PR): m.PR = ToVal<uint>(value); break;
                case nameof(MachineState.SR): m.SR = ToVal<Flagged>(value); break;
                case nameof(MachineState.SSR): m.SSR = ToVal<uint>(value); break;
                case nameof(MachineState.PC): m.PC = ToVal<uint>(value); break;
                case nameof(MachineState.SPC): m.SPC = ToVal<uint>(value); break;
                case nameof(MachineState.T): m.T = ToVal<bool>(value); break;
                case nameof(MachineState.S): m.S = ToVal<bool>(value); break;
                case nameof(MachineState.Q): m.Q = ToVal<bool>(value); break;
                case nameof(MachineState.M): m.M = ToVal<bool>(value); break;
                case nameof(MachineState.I0): m.I0 = ToVal<bool>(value); break;
                case nameof(MachineState.I1): m.I1 = ToVal<bool>(value); break;
                case nameof(MachineState.I2): m.I2 = ToVal<bool>(value); break;
                case nameof(MachineState.I3): m.I3 = ToVal<bool>(value); break;
                case nameof(MachineState.U8): m.U8[ToVal<uint>(arg!)] = ToVal<byte>(value); break;
                case nameof(MachineState.U16): m.U16[ToVal<uint>(arg!)] = ToVal<ushort>(value); break;
                case nameof(MachineState.U32): m.U32[ToVal<uint>(arg!)] = ToVal<uint>(value); break;
                default: throw new InvalidOperationException(name);
            }
        }
        
        public static T ToVal<T>(object value)
        {
            var type = typeof(T).Name;
            object res;
            switch (type)
            {
                case nameof(Flagged):
                    res = value switch
                    {
                        Flagged f => f,
                        uint fu => fu,
                        _ => Convert.ToUInt32($"{value}", 16)
                    };
                    break;
                case nameof(UInt32):
                    res = value is uint i ? i : Convert.ToUInt32($"{value}", 16);
                    break;
                case nameof(UInt16):
                    res = value is ushort u ? u : Convert.ToUInt16($"{value}", 16);
                    break;
                case nameof(Byte):
                    res = value is byte y ? y : Convert.ToByte($"{value}", 16);
                    break;
                case nameof(Boolean):
                    res = value is bool b ? b : Convert.ToByte($"{value}") == 1;
                    break;
                default:
                    throw new InvalidOperationException($"{type} | {value}");
            }
            return (T)res;
        }
    }
}