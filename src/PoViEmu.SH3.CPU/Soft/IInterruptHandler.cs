namespace PoViEmu.SH3.CPU.Soft
{
    public interface IInterruptHandler
    {
        void Handle(byte num, MachineState m);
    }
}