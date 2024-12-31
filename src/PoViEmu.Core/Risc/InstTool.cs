using System;
using System.Collections.Generic;

namespace PoViEmu.Core.Risc
{
    public static class InstTool
    {
        public static Instruction Create(byte first, byte second, Mnemonic code,
            byte? i = null, ushort? d = null, byte? n = null, byte? m = null, byte? ui = null,
            bool nIsRef = false, bool nIsRefP = false, bool nIsRefM = false,
            bool mIsRef = false, bool mIsRefP = false, bool mIsRefM = false,
            params BaseOperand[] a)
        {
            byte[] bytes = [first, second];
            var args = new List<BaseOperand>();
            if (i is { } xI)
                args.Add(new ImmedOperand((sbyte)xI));
            if (ui is { } xUi)
                args.Add(new ImmedUOperand(xUi));
            if (m is { } xM)
                args.Add(mIsRef || mIsRefP || mIsRefM
                    ? new MemoryOperand(GetReg(xM), IsPlus: mIsRefP, IsMinus: mIsRefM)
                    : new SourceReg(GetReg(xM)));
            if (n is { } xN)
                args.Add(nIsRef || nIsRefP || nIsRefM
                    ? new MemoryOperand(GetReg(xN), IsPlus: nIsRefP, IsMinus: nIsRefM)
                    : new DestReg(GetReg(xN)));
            if (d is { } xD)
                args.Add(new DisplOperand(xD));
            if (a is { } xA)
                args.AddRange(xA);
            return new Instruction(bytes, code, args.ToArray());
        }

        private static ShRegister GetReg(byte i)
        {
            return i switch
            {
                0 => ShRegister.R0,
                1 => ShRegister.R1,
                2 => ShRegister.R2,
                3 => ShRegister.R3,
                4 => ShRegister.R4,
                5 => ShRegister.R5,
                6 => ShRegister.R6,
                7 => ShRegister.R7,
                8 => ShRegister.R8,
                9 => ShRegister.R9,
                10 => ShRegister.R10,
                11 => ShRegister.R11,
                12 => ShRegister.R12,
                13 => ShRegister.R13,
                14 => ShRegister.R14,
                15 => ShRegister.R15,
                _ => throw new InvalidOperationException($"{i} ?!")
            };
        }

        public static void LoadSecIfNeeded(this IReader reader, ref byte second, ref bool hadSec)
        {
            if (hadSec)
                return;
            second = reader.ReadNextByte();
            hadSec = true;
        }

        public static (byte high, byte low) SplitByte(byte val)
        {
            var high = val >> 4;
            var low = val & 0b00001111;
            return ((byte)high, (byte)low);
        }

        public static ushort CombineBytes(byte first, byte second)
        {
            var res = (first << 8) | second;
            return (ushort)res;
        }

        public static BaseOperand R0Gbr
            => new MemoryOperand(ShRegister.R0, ShRegister.GBR);

        public static BaseOperand Gbr => new DestReg(ShRegister.GBR);
        public static BaseOperand Mach => new DestReg(ShRegister.MACH);
        public static BaseOperand Macl => new DestReg(ShRegister.MACL);
        public static BaseOperand Pr => new DestReg(ShRegister.PR);
        public static BaseOperand Spc => new DestReg(ShRegister.SPC);
        public static BaseOperand Sr => new DestReg(ShRegister.SR);
        public static BaseOperand Ssr => new DestReg(ShRegister.SSR);
        public static BaseOperand Vbr => new DestReg(ShRegister.VBR);

        public static BaseOperand R0 => new DestReg(ShRegister.R0);

        public static BaseOperand R0Bank => new DestReg(ShRegister.R0_Bank);
        public static BaseOperand R1Bank => new DestReg(ShRegister.R1_Bank);
        public static BaseOperand R2Bank => new DestReg(ShRegister.R2_Bank);
        public static BaseOperand R3Bank => new DestReg(ShRegister.R3_Bank);
        public static BaseOperand R4Bank => new DestReg(ShRegister.R4_Bank);
        public static BaseOperand R5Bank => new DestReg(ShRegister.R5_Bank);
        public static BaseOperand R6Bank => new DestReg(ShRegister.R6_Bank);
        public static BaseOperand R7Bank => new DestReg(ShRegister.R7_Bank);

        public static BaseOperand N(byte low) => new DestReg(GetReg(low));
        public static BaseOperand M(byte low) => new SourceReg(GetReg(low));
        public static BaseOperand I(byte low) => new ImmedUOperand(low);
        public static BaseOperand D(ushort low) => new DisplOperand(low);
        
        public static BaseOperand Nm(byte low) => new MemoryOperand(GetReg(low), IsMinus: true);
        public static BaseOperand M(byte low, ushort dis) => new MemoryOperand(GetReg(low), Dis: dis);
        public static BaseOperand M(ShRegister low, ushort dis) => new MemoryOperand(low, Dis: dis);
        public static BaseOperand M(byte low, ShRegister off) => new MemoryOperand(off, GetReg(low));
    }
}