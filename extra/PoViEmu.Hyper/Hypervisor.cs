using System;
using System.Collections.Generic;
using static PoViEmu.Base.StreamHelper;

namespace PoViEmu.Hyper
{
    public sealed class Hypervisor : IHypervisor
    {
        public static IHypervisor Default { get; } = new Hypervisor();

        private readonly IDictionary<Guid, IVMachine> _machines;

        private Hypervisor()
        {
            _machines = new Dictionary<Guid, IVMachine>();
        }

        public IVMachine Create(VMConfig config)
        {
            IVMachine machine;
            var kind = config.Kind;
            switch (kind)
            {
                case CpuKind.SH3: machine = new ShMachine(S["op_sh3.com"]); break;
                case CpuKind.X86: machine = new NcMachine(S["op_x86.com"]); break;
                default: throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
            return _machines[config.Id] = machine;
        }

        public IVMachine? GetRunning(Guid id)
        {
            _machines.TryGetValue(id, out var found);
            return found;
        }

        private static readonly IDictionary<string, byte[]> S = GetManifestResources<Hypervisor>("Counter");
    }
}