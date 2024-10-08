namespace PoViEmu.Core.Decoding.Ops.Consts
{
    public abstract record NumOperand<T> : BaseOperand
    {
        public abstract T Val { get; init; }
    }
}