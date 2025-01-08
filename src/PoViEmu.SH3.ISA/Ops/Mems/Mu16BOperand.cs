namespace PoViEmu.SH3.ISA.Ops.Mems
{
    public record Mu16BOperand(ShRegister Seg, ShRegister? Base, ShRegister? Idx, short? Disp)
        : MemOperand<(ushort, ushort)>(Seg, Base, Idx, Disp)
    {
        public override (ushort, ushort) this[IMachineState m]
        {
            get
            {
                // TODO
                var a = m.U16[this.OffA(m)];
                var b = m.U16[this.OffA(m)];
                return (a, b);
            }
            set
            {
                // TODO
                var (a, b) = value;
                m.U16[this.OffA(m)] = a;
                m.U16[this.OffA(m)] = b;
            }
        }

        public override string ToDebug(bool v) => ToString();
    }
}