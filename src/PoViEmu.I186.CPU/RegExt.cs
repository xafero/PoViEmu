using PoViEmu.I186.ISA.Ops.Regs;

namespace PoViEmu.I186.CPU
{
    public static class RegExt
    {
        public static byte Get(this MachineState m, Reg8Operand reg)
            => m.Get(reg.Reg);
        
        public static ushort Get(this MachineState m, Reg16Operand reg)
            => m.Get(reg.Reg);
        
        public static void Set(this MachineState m, Reg8Operand reg, byte val)
            => m.Set(reg.Reg, val);
        
        public static void Set(this MachineState m, Reg16Operand reg, ushort val)
            => m.Set(reg.Reg, val);
    }
}