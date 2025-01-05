namespace PoViEmu.SH3.ISA.Ops
{
    public abstract record BaseOperand()
    {
        public abstract string ToDebug(bool v);
    }
}