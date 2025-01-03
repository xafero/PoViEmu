using PoViEmu.Base.ISA;

namespace PoViEmu.Base.CPU
{
    public interface ICpuFactory<TI, TS>
        where TI : IInstruction
        where TS : IState
    {
        ICpu<TI, TS> CreateCpu(byte[] bytes, out TS state);

        ICodeReader<TI> CreateReader(TS state);
    }
}