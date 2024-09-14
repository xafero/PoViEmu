using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Iced.Intel;

namespace PoViEmu.Core.Hardware
{
    public static class IceFun
    {
        private static readonly List<OpKind> ImmediateKinds =
        [
            OpKind.Immediate8,
            OpKind.Immediate8_2nd,
            OpKind.Immediate8to16,
            OpKind.Immediate16
        ];

        private static readonly List<IceRegister> RegisterMapping =
        [
            new(Register.AX, Register.AH, Register.AL),
            new(Register.BX, Register.BH, Register.BL),
            new(Register.CX, Register.CH, Register.CL),
            new(Register.DX, Register.DH, Register.DL),
            new(Register.SI, Register.None, Register.SIL),
            new(Register.DI, Register.None, Register.DIL),
            new(Register.BP, Register.None, Register.BPL),
            new(Register.SP, Register.None, Register.SPL)
        ];

        public static string GetName(this Register register) => register.ToString().ToLower();

        public static int GetSizeInBits(this Register register) => register.GetSize() * 8;

        public static bool IsImmediate(this OpKind kind) => ImmediateKinds.Contains(kind);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBranchOpKind(this OpKind kind)
        {
            switch (kind)
            {
                case OpKind.FarBranch16:
                case OpKind.NearBranch16:
                    return true;
                default:
                    return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong? GetBranchTarget(this Instruction i, OpKind kind)
        {
            switch (kind)
            {
                case OpKind.FarBranch16: return i.FarBranch16;
                case OpKind.NearBranch16: return i.NearBranch16;
                default: return null;
            }
        }
    }
}