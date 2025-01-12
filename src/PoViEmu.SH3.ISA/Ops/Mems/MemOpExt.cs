using System;
using MS = PoViEmu.SH3.ISA.IMachineState;
using R = PoViEmu.SH3.ISA.ShRegister;

namespace PoViEmu.SH3.ISA.Ops.Mems
{
    public static class MemOpExt
    {
        public static uint OffA(this MemOperand op, MS s, bool allowMod = true)
        {
            uint offset;
            switch (op.Mode)
            {
                case AddressingMode.RegIndirect:
                    offset = s[op.Base];
                    break;
                case AddressingMode.PostIncrement:
                    offset = s[op.Base];
                    if (allowMod) s[op.Base] += op.ByteSize;
                    break;
                case AddressingMode.PreDecrement:
                    if (allowMod) s[op.Base] -= op.ByteSize;
                    offset = s[op.Base];
                    break;
                case AddressingMode.Displacement:
                    offset = (uint)(s[op.Base] + op.Disp!);
                    break;
                case AddressingMode.Indexed:
                    offset = s[op.Base] + s[(R)op.Idx!];
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"{op.GetType().Name} {op.Mode}");
            }
            return offset;
        }
    }
}