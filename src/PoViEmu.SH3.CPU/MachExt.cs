using System;
using PoViEmu.SH3.ISA;
using PoViEmu.SH3.ISA.Ops;

namespace PoViEmu.SH3.CPU
{
    public static class MachExt
    {
        public static uint Get(MachineState m, ShRegister r)
        {
            throw new InvalidOperationException($"{r} ?!");
        }

        public static uint Get(MachineState m, RegOperand r)
        {
            switch (r)
            {
                case SourceReg sr: return Get(m, sr.Reg);
                case DestReg dr: return Get(m, dr.Reg);
            }
            throw new InvalidOperationException($"{r} ?!");
        }

        public static void Set(MachineState m, ShRegister r, uint value)
        {
            throw new InvalidOperationException($"{r} {value} ?!");
        }

        public static void Set(MachineState m, RegOperand r, uint value)
        {
            switch (r)
            {
                case SourceReg sr: Set(m, sr.Reg, value); break;
                case DestReg dr: Set(m, dr.Reg, value); break;
            }
            throw new InvalidOperationException($"{r} {value} ?!");
        }

        public static long ReadByte(MachineState s, uint offset)
        {
            throw new NotImplementedException();
        }

        public static short ReadWord(MachineState s, uint offset)
        {
            throw new NotImplementedException();
        }
        
        public static int ReadLong(MachineState s, uint offset)
        {
            throw new NotImplementedException();
        }
        
        public static void WriteByte(MachineState s, uint a, uint b)
        {
            throw new NotImplementedException();
        }

        public static void WriteWord(MachineState s, uint a, uint b)
        {
            throw new NotImplementedException();
        }

        public static void WriteLong(MachineState s, uint a, uint b)
        {
            throw new NotImplementedException();
        }
    }
}