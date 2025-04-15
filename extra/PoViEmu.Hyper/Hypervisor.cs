using System;

namespace PoViEmu.Hyper
{
    public sealed class Hypervisor : IHypervisor
    {
        public static IHypervisor Default { get; } = new Hypervisor();

        private Hypervisor()
        {
        }

        public IVMachine Create(CpuKind kind)
        {
            switch (kind)
            {
                case CpuKind.SH3: return new ShMachine();
                case CpuKind.X86: return new NcMachine();
                default: throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
        }
    }
}