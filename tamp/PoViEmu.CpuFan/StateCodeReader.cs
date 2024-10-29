using System;
using Iced.Intel;
using PoViEmu.Core.Hardware;

namespace PoViEmu.CpuFan
{
    public sealed class StateCodeReader : CodeReader
    {
        private readonly MachineState _parent;

        public StateCodeReader(MachineState parent)
        {
            _parent = parent;
        }

        public override int ReadByte()
        {
            Console.WriteLine("ReadByte ?");

            throw new NotImplementedException();
        }
    }
}