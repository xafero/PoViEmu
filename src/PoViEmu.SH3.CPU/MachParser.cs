using System;
using static PoViEmu.Base.PropHelper;

namespace PoViEmu.SH3.CPU
{
    public static class MachParser
    {
        public static object GetByString(this MachineState m, string? name)
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
                nameof(MachineState.PR) => m.PR,
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
            switch (key?.ToLowerInvariant())
            {
                default: throw new InvalidOperationException(name);
            }
        }
    }
}