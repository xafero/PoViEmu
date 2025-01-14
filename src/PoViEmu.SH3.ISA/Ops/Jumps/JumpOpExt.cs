using System;

// ReSharper disable InconsistentNaming

namespace PoViEmu.SH3.ISA.Ops.Jumps
{
    public static class JumpOpExt
    {
        public static void Jump(this JumpOperand op, ref uint nextIP)
        {
            switch (op)
            {
                case NearOperand no:
                    nextIP = no.NextOff(nextIP);
                    return;
                case FarOperand fo:
                    nextIP = fo.Off;
                    return;
            }
            throw new InvalidOperationException($"{op} ?!");
        }
    }
}