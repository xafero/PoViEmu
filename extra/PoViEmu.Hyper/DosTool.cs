using System;
using PoViEmu.Base.CPU;
using PoViEmu.I186.CPU;
using PoViEmu.I186.CPU.Soft;
using PoViEmu.SH3.CPU;
using PoViEmu.SH3.CPU.Soft;

namespace PoViEmu.Hyper
{
    public static class DosTool
    {
        public static IDosEmu GetDosEmu(this ICpu cpu)
        {
            return cpu switch
            {
                SH7291 sh => sh.GetDOS(),
                NC3022 nc => nc.GetDOS(),
                _ => throw new InvalidOperationException($"Not supported ({cpu})!")
            };
        }
    }
}