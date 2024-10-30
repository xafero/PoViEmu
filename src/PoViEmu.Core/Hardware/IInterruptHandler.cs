namespace PoViEmu.Core.Hardware
{
    public interface IInterruptHandler
    {
        void Handle(byte num, MachineState m);
    }
}