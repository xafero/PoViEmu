using System.ComponentModel;

namespace PoViEmu.Core.Meta
{
    public enum DeviceModel
    {
        Unknown = 0,

        /// <summary>
        /// PV-S460
        /// </summary>
        [Description("PV-S460")] S460,

        /// <summary>
        /// PV-S660
        /// </summary>
        [Description("PV-S660")] S660
    }
}