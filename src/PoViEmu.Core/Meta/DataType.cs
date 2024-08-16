namespace PoViEmu.Core.Meta
{
    public enum DataType
    {
        Unknown = 0,

        /// <summary>
        /// 1 char
        /// </summary>
        Character,

        /// <summary>
        /// 1-byte hexadecimal display
        /// </summary>
        Byte,

        /// <summary>
        /// 2-byte hexadecimal display
        /// </summary>
        Word,

        /// <summary>
        /// 4-byte hexadecimal display
        /// </summary>
        DWord,

        /// <summary>
        /// 2-byte decimal display
        /// </summary>
        Integer,

        /// <summary>
        /// string divided by NULL
        /// </summary>
        StringCharacter
    }
}