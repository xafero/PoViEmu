namespace PoViEmu.Base
{
    /// <summary>
    /// The byte order (endianness) used to store data in memory
    /// </summary>
    public enum EndianMode
    {
        /// <summary>
        /// Big-endian byte order
        /// (the most significant byte is stored first)
        /// </summary>
        BigEndian = 0,

        /// <summary>
        /// Little-endian byte order
        /// (the least significant byte is stored first)
        /// </summary>
        LittleEndian = 1
    }
}