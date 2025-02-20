using System;
using System.Threading.Tasks;
using PoViEmu.Base;
using PoViEmu.Inventory.Upper;
using PoViEmu.Inventory.Utils;

namespace PoViEmu.Inventory.Config
{
    public sealed class CfgRepo
    {
        public static CfgRepo Instance { get; } = new();

        public async Task Load()
        {
            var inst = AppConst.Instance;
            var root = inst.DataRoot;

            var cfgFile = root.MakeDirFor("global.json", "data", "config");
            Global = await CacheHelper.GetCachedJson(Init, cfgFile);
        }

        public GlobalConfig? Global { get; private set; }

        private static GlobalConfig Init()
        {
            var inst = AppConst.Instance;
            var root = inst.DataRoot;

            var config = new GlobalConfig
            {
                Created = DateTime.Now,
                InstanceDir = root.MakeDirFor("", "data", "instances")
            };
            return config;
        }

        public DirDict<OneEntity>? Entities
            => Global?.InstanceDir is { } root
                ? new DirDict<OneEntity>(root, "entity.json")
                : null;
    }
}