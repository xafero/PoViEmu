using System.Collections.Generic;
using PoViEmu.Base.CPU;
using CsI186 = PoViEmu.I186.CPU.Impl.ICodeState;
using CsSH3 = PoViEmu.SH3.CPU.Impl.ICodeState;

namespace PoViEmu.UI.Models
{
    public sealed class FakeState : CsI186, CsSH3
    {
        public ushort CS { get; set; }
        public ushort IP { get; set; }

        public IEnumerable<byte> ReadMemory(ushort segment, ushort offset, int count)
            => (Wrapped as CsI186)?.ReadMemory(segment, offset, count)!;

        public uint PC { get; set; }
        public uint? dPC { get; set; }

        public IEnumerable<byte> ReadMemory(uint offset, int count)
            => (Wrapped as CsSH3)?.ReadMemory(offset, count)!;

        public IState Wrapped { get; set; }
    }
}