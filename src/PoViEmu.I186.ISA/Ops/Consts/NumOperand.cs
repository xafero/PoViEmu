namespace PoViEmu.I186.ISA.Ops.Consts
{
    public abstract record NumOperand<T> : BaseOperand
    {
        public abstract T Val { get; init; }
    }
}