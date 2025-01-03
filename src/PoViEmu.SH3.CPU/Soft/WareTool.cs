// ReSharper disable InconsistentNaming

using PoViEmu.SH3.CPU;
using PoViEmu.SH3.CPU.Soft;

namespace PoViEmu.I186.CPU.Soft
{
    public static class WareTool
    {
        public static DOSInterrupts GetDOS(this SH7291 c)
        {
            var handler = c.InterruptTable[0x21];
            return (DOSInterrupts)handler;
        }
    }
}