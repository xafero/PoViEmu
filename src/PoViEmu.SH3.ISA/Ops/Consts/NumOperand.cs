namespace PoViEmu.SH3.ISA.Ops.Consts
{
    public abstract record NumOperand<T> : BaseOperand
    {
        public abstract T Val { get; init; }
    }
}