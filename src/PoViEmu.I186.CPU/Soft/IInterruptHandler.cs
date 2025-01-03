namespace PoViEmu.I186.CPU.Soft
{
    public interface IInterruptHandler
    {
        void Handle(byte num, MachineState m);
    }
}