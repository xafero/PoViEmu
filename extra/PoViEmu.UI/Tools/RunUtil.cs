using System;
using System.Linq;
using System.Threading.Tasks;
using PoViEmu.Hyper;
using PoViEmu.Inventory.Config;
using PoViEmu.Inventory.Infos;
using PoViEmu.Inventory.Upper;

namespace PoViEmu.UI.Tools
{
    public static class RunUtil
    {
        public record RunObj(OneEntity Entity, TemplEntry Template)
        {
            public CpuKind ProcessorKind => Template.Kind.GetProcessorKind();
        }

        public static async Task<RunObj?> FindEntity(Guid instanceId)
        {
            var repo = CfgRepo.Instance;
            await repo.Load();
            var entity = repo.Entities?[instanceId];
            if (entity == null)
                return null;
            var tmpl = TemplRepo.Instance;
            await tmpl.Load();
            var allTemplates = tmpl.AllTemplates;
            var entry = allTemplates?.FirstOrDefault(t => t.Name == entity.Template);
            if (entry == null)
                return null;
            return new RunObj(entity, entry);
        }

        public static CpuKind GetProcessorKind(this ModelKind kind)
        {
            return kind switch
            {
                ModelKind.X86 => CpuKind.X86,
                ModelKind.SH3 => CpuKind.SH3,
                _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null)
            };
        }
    }
}