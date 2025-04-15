namespace PoViEmu.Hyper
{
    public interface IHypervisor
    {
        IVMachine Create(VMConfig config);
    }
}