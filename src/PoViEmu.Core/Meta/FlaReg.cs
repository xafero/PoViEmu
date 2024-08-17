using System.Collections.Generic;

namespace PoViEmu.Core.Meta
{
    public sealed class FlaReg
    {
        public FlaReg()
        {
            Registers = new Dictionary<CpuRegister, ushort>();
            Flags = new Dictionary<CpuFlag, bool>();
        }

        public Dictionary<CpuRegister, ushort> Registers { get; }
        public Dictionary<CpuFlag, bool> Flags { get; }
    }
}