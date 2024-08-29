namespace PoViEmu.Core.Addins
{
    public enum AddInMode
    {
        Unknown = 0,

        /// <summary>
        /// IB_MADDIN, IW_MADDIN
        /// </summary>
        SdkBuilt = 0x08FF,

        /// <summary>
        /// IB_MCLOCK, IW_MCLOCK
        /// </summary>
        Clock = 0x0400,

        /// <summary>
        /// IB_MSCHD, IW_MSCHD
        /// </summary>
        Schedule = 0x0300,

        /// <summary>
        /// IB_SMCALE
        /// </summary>
        Calendar = 0x0305,

        /// <summary>
        /// IB_MCOM, IW_MCOM
        /// </summary>
        Communication = 0x0700,

        /// <summary>
        /// IB_MMENUMD, IW_MMENUMD
        /// </summary>
        Menu = 0x0E00,

        /// <summary>
        /// IB_MCONV, IW_MCONV
        /// </summary>
        Conversion = 0x0D00,

        /// <summary>
        /// IB_MMEMO, IW_MMEMO
        /// </summary>
        Memo = 0x0200,

        /// <summary>
        /// IB_MEXPEN, IW_MEXPEN
        /// </summary>
        Expense = 0x0C00,

        /// <summary>
        /// IB_MTEL, IW_MTEL
        /// </summary>
        Contacts = 0x0100,

        /// <summary>
        /// IB_SMLIBINT
        /// </summary>
        LibInt = 0x0080,

        /// <summary>
        /// IB_SMFUNCINT
        /// </summary>
        FuncInt = 0x0081,

        /// <summary>
        /// IB_SMSYSDAT1
        /// </summary>
        SystemData1 = 0x0082,

        /// <summary>
        /// IB_SMSYSDAT2
        /// </summary>
        SystemData2 = 0x0083,

        /// <summary>
        /// IW_MSECRET
        /// </summary>
        Secret = 0x8000,

        /// <summary>
        /// IW_MQFORM 
        /// </summary>
        QuickForm = 0x0900,

        /// <summary>
        /// IW_MMAIL
        /// </summary>
        Mail = 0x1000,

        /// <summary>
        /// IW_MGAME
        /// </summary>
        Game = 0x0A00
    }
}