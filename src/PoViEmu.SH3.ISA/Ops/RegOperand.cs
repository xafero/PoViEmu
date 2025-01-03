using PoViEmu.SH3.ISA.Core;

namespace PoViEmu.SH3.ISA.Ops
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