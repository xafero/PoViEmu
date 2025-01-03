namespace PoViEmu.I186.ABI
{
    public enum AddInModel
    {
        Unknown = 0,

        /// <summary>
        /// working on all PV models
        /// </summary>
        Z486,

        /// <summary>
        /// working only with PV 750(+)
        /// </summary>
        Z488,

        /// <summary>
        /// working only for PV-S460/S660
        /// </summary>
        G500
    }
}