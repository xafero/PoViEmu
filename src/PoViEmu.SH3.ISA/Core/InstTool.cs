using System;
using System.Collections.Generic;
using PoViEmu.SH3.ISA.Decoding;
using PoViEmu.SH3.ISA.Ops;

namespace PoViEmu.SH3.ISA.Core
{
    internal static class InstTool
    {
        internal static ShRegister GetReg(byte i)
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

        public static Instruction Create(byte first, byte second, Mnemonic code, params BaseOperand[] a)
        {
            byte[] bytes = [first, second];
            var args = new List<BaseOperand>();
            if (a is { } xA) args.AddRange(xA);
            return new Instruction(bytes, code, args.ToArray());
        }

        public static void LoadSecIfNeeded(this IByteReader reader, ref byte second, ref bool hadSec)
        {
            if (hadSec)
                return;
            second = reader.ReadByte();
            hadSec = true;
        }

        public static (byte high, byte low) SplitByte(byte val)
        {
            var high = val >> 4;
            var low = val & 0b00001111;
            return ((byte)high, (byte)low);
        }

        public static int CombineBytes(byte first, byte second)
        {
            var tmp = (first << 8) | second;
            if (tmp >= 0x800)
            {
                var signed12 = tmp - 0x1000;
                return signed12;
            }
            var unsigned12 = tmp;
            return unsigned12;
        }
    }
}