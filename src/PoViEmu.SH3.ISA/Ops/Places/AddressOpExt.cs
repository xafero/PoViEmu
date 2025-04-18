using System;
using MS = PoViEmu.SH3.ISA.IMachineState;
using R = PoViEmu.SH3.ISA.ShRegister;

namespace PoViEmu.SH3.ISA.Ops.Places
{
    public static class AddressOpExt
    {
        public static uint OffA(this IAddressOperand op, MS s)
        {
            uint offset;
            switch (op.Mode)
            {
                case AddressingMode.RegIndirect:
                    offset = s[op.Base];
                    break;
                case AddressingMode.PostIncrement:
                    offset = s[op.Base];
                    s[op.Base] += op.ByteSize;
                    break;
                case AddressingMode.PreDecrement:
                    s[op.Base] -= op.ByteSize;
                    offset = s[op.Base];
                    break;
                case AddressingMode.Displacement:
                    offset = (uint)(s[op.Base] + op.Disp!);
                    break;
                case AddressingMode.Relative:
                    offset = (uint)(GetAligned(s, op.Base, op.Align) + op.Disp!);
                    break;
                case AddressingMode.Indexed:
                    offset = s[op.Base] + s[(R)op.Idx!];
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"{op.GetType().Name} {op.Mode}");
            }
            return offset;
        }

        private static uint GetAligned(MS s, R @base, bool isAligned)
        {
            var relBase = s[@base];
            if (!isAligned)
                return relBase;

            var aligned = relBase % 4;
            if (aligned != 0)
                relBase -= aligned;
            return relBase;
        }
    }
}