using PoViEmu.Core.Hardware;
using PoViEmu.Core.Hardware.AckNow;

namespace PoViEmu.Core.Decoding.Ops.Mems
{
    public record Mu16BOperand(B16Register Seg, B16Register? Base, B16Register? Idx, short? Disp)
        : MemOperand<(ushort, ushort)>(Seg, Base, Idx, Disp)
    {
        public override (ushort, ushort) this[MachineState m]
        {
            get
            {
                // TODO
                var a = m.U16[this.SegA(m), this.OffA(m)];
                var b = m.U16[this.SegA(m), this.OffA(m)];
                return (a, b);
            }
            set
            {
                // TODO
                var (a, b) = value;
                m.U16[this.SegA(m), this.OffA(m)] = a;
                m.U16[this.SegA(m), this.OffA(m)] = b;
            }
        }
    }
}