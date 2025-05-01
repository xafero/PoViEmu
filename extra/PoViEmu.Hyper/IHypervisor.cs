using System;

namespace PoViEmu.Hyper
{
    public interface IHypervisor
    {
        IVMachine Create(VMConfig config);

        IVMachine? GetRunning(Guid id);
    }
}