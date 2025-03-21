using MS = PoViEmu.I186.ISA.IMachineState;

namespace PoViEmu.I186.ISA.Ops.Mems
{
    public static class MemOpExt
    {
        public static ushort SegA(this MemOperand op, MS m)
        {
            var segA = m[op.Seg];
            return segA;
        }

        public static ushort OffA(this MemOperand op, MS m)
        {
            var realB = op.Base is { } oB ? m[oB] : 0;
            var realI = op.Idx is { } oI ? m[oI] : 0;
            var realD = op.Disp ?? 0;

            var realA = realB + realI + realD;
            var realS = (ushort)realA;
            return realS;
        }
    }
}