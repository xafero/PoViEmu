using Cscg.AutoGen;
using PoViEmu.Common;

namespace PoViEmu.Core.Inventory
{
    public sealed class AppConst
    {
        public static AppConst Instance { get; } = new();

        private AppConst()
        {
            DataRoot = PathHelper.CurrentDir;
            BaseUrl = ConstStrings.DefaultRepoBase;
        }

        public string DataRoot { get; }
        public string BaseUrl { get; }
    }
}