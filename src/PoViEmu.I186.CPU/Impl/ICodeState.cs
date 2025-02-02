// ReSharper disable InconsistentNaming

using System.Collections.Generic;

namespace PoViEmu.I186.CPU.Impl
{
    public interface ICodeState
    {
        ushort CS { get; }

        ushort IP { get; }

        IEnumerable<byte> ReadMemory(ushort segment, ushort offset, int count);
    }
}