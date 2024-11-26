using System;

// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Decoding.Ops.Jumps
{
    public static class JumpOpExt
    {
        public static void Jump(this JumpOperand op, ref ushort nextIP)
        {
            switch (op)
            {
                case NearOperand no:
                    nextIP = no.NextOff(nextIP);
                    return;
            }
            throw new InvalidOperationException($"{op} ?!");
        }

        public static void Jump(this JumpOperand op, ref ushort nextSeg, ref ushort nextIP)
        {
            switch (op)
            {
                case FarOperand fo:
                    nextSeg = fo.Seg;
                    nextIP = fo.Off;
                    return;
            }
            throw new InvalidOperationException($"{op} ?!");
        }
    }
}