using PoViEmu.Base.ISA;

namespace PoViEmu.Base.CPU
{
    public interface ICpu
    {
        bool Halted { get; set; }
    }

    public interface ICpu<in TI, in TS> : ICpu
        where TI : IInstruction
        where TS : IState
    {
        void Execute(TI instr, TS state);
    }
}