namespace PoViEmu.Core.Dumps
{
    public enum DumpModel
    {
        Unknown = 0,

        /// <summary>
        /// PV-250X or PV-450X since 2000
        /// </summary>
        Z486,

        /// <summary>
        /// PV-750 or PV-750 Plus since 2000 
        /// </summary>
        Z488,

        /// <summary>
        /// PV-S250 or PV-S450 since 2001
        /// </summary>
        Z481,

        /// <summary>
        /// PV-S460 or PV-S660 since 2002
        /// </summary>
        G500,

        /// <summary>
        /// PV-S400 Plus or PV-S600 Plus since 2002
        /// </summary>
        G501
    }
}