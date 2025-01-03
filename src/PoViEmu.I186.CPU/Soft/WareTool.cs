// ReSharper disable InconsistentNaming

namespace PoViEmu.I186.CPU.Soft
{
    public static class WareTool
    {
        public static DOSInterrupts GetDOS(this NC3022 c)
        {
            var handler = c.InterruptTable[0x21];
            return (DOSInterrupts)handler;
        }
    }
}