using MS = PoViEmu.SH3.ISA.IMachineState;

namespace PoViEmu.SH3.ISA.Ops.Mems
{
    public static class MemOpExt
    {
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