using PoViEmu.Base.ISA;

namespace PoViEmu.Base.CPU
{
    public interface IValFormatter
    {
        string Format(object value);
        
        string GetFull(IInstruction instr, IState state);
        
        string GetAdrTitle();
    }
}