namespace PoViEmu.Core.Risc
{
    public abstract record RegOperand : BaseOperand
    {
        public abstract ShRegister Reg { get; init; }

        public sealed override string ToString()
        {
            var name = Reg.Name();
            return name;
        }
    }
}