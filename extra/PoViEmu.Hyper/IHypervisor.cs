namespace PoViEmu.Hyper
{
    public interface IHypervisor
    {
        IVMachine Create(CpuKind kind);
    }
}