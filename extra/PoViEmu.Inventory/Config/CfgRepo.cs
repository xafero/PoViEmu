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

        private static GlobalConfig Init()
        {
            var config = new GlobalConfig { Created = DateTime.Now };
            return config;
        }

        public GlobalConfig? Global { get; private set; }
    }
}