﻿using System;

// ReSharper disable InconsistentNaming

namespace PoViEmu.SH3.ISA.Ops.Jumps
{
    public abstract record JumpOperand : BaseOperand
    {
    }

    public record NearOperand(int Diff)
        : JumpOperand
    {
        public uint NextOff(uint nextIP)
        {
            var callDst = nextIP + Diff;
            var destIP = (uint)callDst;
            return destIP;
        }
        
        public override string ToString()
        {
            return $"0x{Diff:x8}";
        }
    }

    public record FarOperand(uint Off)
        : JumpOperand
    {
        public override string ToString()
        {
            return $"0x{Off:x8}";
        }
    }

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