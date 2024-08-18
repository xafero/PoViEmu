using System.Collections.Generic;

namespace PoViEmu.Core.Meta
{
    public sealed class Ems
    {
        public Ems()
        {
            Banks = new Dictionary<EmsBank, ushort>();
            Frames = new Dictionary<EmsFrame, ushort>();
        }

        public Dictionary<EmsBank, ushort> Banks { get; }
        public Dictionary<EmsFrame, ushort> Frames { get; }

        public void Clear()
        {
            Banks.Clear();
            Frames.Clear();
        }
    }
}