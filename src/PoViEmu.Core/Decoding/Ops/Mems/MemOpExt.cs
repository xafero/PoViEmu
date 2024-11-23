using System;
using PoViEmu.Core.Hardware;

namespace PoViEmu.Core.Decoding.Ops.Mems
{
    public static class MemOpExt
    {
        public static ushort SegA(this MemOperand op, MachineState m)
        {
            var segA = m[op.Seg];
            return segA;
        }

        public static ushort OffA(this MemOperand op, MachineState m)
        {
            if (op.Disp is { } offI)
            {
                var offV = (ushort)offI;
                return offV;
            }

            if (op.Idx is { } offR)
            {
                var offA = m[offR];
                return offA;
            }

            throw new InvalidOperationException($"{op} ?!");
        }
    }
}