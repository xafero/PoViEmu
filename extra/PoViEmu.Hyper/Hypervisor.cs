using System;
using System.Collections.Generic;
using static PoViEmu.Base.StreamHelper;

namespace PoViEmu.Hyper
{
    public sealed class Hypervisor : IHypervisor
    {
        public static IHypervisor Default { get; } = new Hypervisor();

        private Hypervisor()
        {
        }

        public IVMachine Create(VMConfig config)
        {
            var kind = config.Kind;
            switch (kind)
            {
                case CpuKind.SH3: return new ShMachine(_s["op_sh3.com"]);
                case CpuKind.X86: return new NcMachine(_s["op_x86.com"]);
                default: throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
        }

        private static IDictionary<string, byte[]> _s = GetManifestResources<Hypervisor>("Counter");
    }
}