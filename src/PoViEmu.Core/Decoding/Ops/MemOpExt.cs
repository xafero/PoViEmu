using System;
using PoViEmu.Core.Hardware;

namespace PoViEmu.Core.Decoding.Ops
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
            if (op.Off is { } offI)
            {
                return offI;
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