namespace PoViEmu.SH3.ISA.Ops.Places
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
        Indexed,

        /// <summary>
        /// 0x00000400
        /// </summary>
        Relative
    }
}