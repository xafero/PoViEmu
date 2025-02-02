// ReSharper disable InconsistentNaming

using System.Collections.Generic;

namespace PoViEmu.SH3.CPU.Impl
{
    public interface ICodeState
    {
        uint PC { get; }

        uint? dPC { get; }

        IEnumerable<byte> ReadMemory(uint offset, int count);
    }
}