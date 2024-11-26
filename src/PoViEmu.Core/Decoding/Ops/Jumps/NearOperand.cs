

// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Decoding.Ops.Jumps
{
    public record NearOperand(int Diff)
        : JumpOperand
    {
        public ushort NextOff(ushort nextIP)
        {
            var callDst = nextIP + Diff;
            var destIP = (ushort)callDst;
            return destIP;
        }
    }
}