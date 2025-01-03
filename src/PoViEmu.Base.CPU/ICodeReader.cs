using PoViEmu.Base.ISA;

namespace PoViEmu.Base.CPU
{
    public interface ICodeReader<out T> where T : IInstruction
    {
        T NextInstruction();
    }
}