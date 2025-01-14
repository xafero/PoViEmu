namespace PoViEmu.SH3.ISA.Ops.Mems
{
    public enum AddressingMode
    {
        Unknown = 0,

        /// <summary>
        /// @r5
        /// </summary>
        RegIndirect,

        /// <summary>
        /// @r5+
        /// </summary>
        PostIncrement,

        /// <summary>
        /// @-r5
        /// </summary>
        PreDecrement,

        /// <summary>
        /// @(36,r5)
        /// </summary>
        Displacement,

        /// <summary>
        /// @(r0,r5)
        /// </summary>
        Indexed
    }
}