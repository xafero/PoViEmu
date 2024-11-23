using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MS = PoViEmu.Core.Hardware.MachineState;

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