using PoViEmu.SH3.ISA.Core;

namespace PoViEmu.SH3.ISA.Ops
{
    public abstract record BaseOperand()
    {
        public static implicit operator BaseOperand(ShRegister reg)
            => new Core.RegOperand(reg);

        public static implicit operator BaseOperand((ShRegister reg1, ShRegister reg2) t)
            => new Core.RegRegOperand(t.reg1, t.reg2);

        public abstract string ToDebug(bool v);
    }
}