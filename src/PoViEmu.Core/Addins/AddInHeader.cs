using System.Runtime.InteropServices;

namespace PoViEmu.Core.Addins
{
    [StructLayout(LayoutKind.Sequential)]
    public struct AddInHeader
    {
        /*
         * Second byte (0xFF) works as a flag for deleted Add-Ins.
         * Deleted Add-Ins are marked with a 0x00 at this location.
         */

        /// <summary>
        /// [offset 0x0000]
        /// Signature of the Add-In
        /// {0x00, 0xFF, 'C','A','S','I','O', 0x03}
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] Signature;

        /*
         * Observed model descriptors are "Z486" for Add-Ins working
         * on all PV models, "Z488" for Add-Ins working only with PV 750(+),
         * "G500" for Add-Ins working only for PV-S460/S660.
         */

        /// <summary>
        /// [offset 0x0008]
        /// Model identifier
        /// {'Z','4','8','6'}
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string Model;

        /// <summary>
        /// [offset 0x000C]
        /// Version of the header
        /// {'0','1','0','0'}
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string HeaderVersion;
        
        /// <summary>
        /// [offset 0x0010]
        /// Status of the Add-In
        /// </summary>
        public ushort Status;

        /*
        * The submode will be changed to a value from 0x01 to 0x0F during installation.
        */

        /// <summary>
        /// [offset 0x0012]
        /// Mode and submode of the Add-In
        /// </summary>
        public ushort Mode;

        /// <summary>
        /// [offset 0x0014]
        /// Name of the Add-In
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string Name;

        /// <summary>
        /// [offset 0x0024]
        /// Length of the Add-In data
        /// </summary>
        public uint Length;

        /// <summary>
        /// [offset 0x0028]
        /// Date of compilation (YYYYMMDD)
        /// (e.g. "20020219" stands for Feb. 19th, 2002) 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string CompileDate;

        /// <summary>
        /// [offset 0x0030]
        /// Time of compilation (HHMM)
        /// (e.g. "1246" is 46 minutes past 12 o'clock)
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CompileTime;

        /// <summary>
        /// [offset 0x0034]
        /// Version of the Add-In
        /// (e.g. "0120" stands for version 1.20)
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string Version;

        /// <summary>
        /// [offset 0x0038]
        /// Date of library (YYYYMMDD)
        /// (e.g. "20000215" stands for Feb. 15th, 2000)
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string LibraryDate;

        /// <summary>
        /// [offset 0x0040]
        /// Time of library compilation (HHMM)
        /// (e.g. "0940" is 40 minutes past 9 o'clock) 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string LibraryTime;

        /// <summary>
        /// [offset 0x0044]
        /// Version of the library
        /// (e.g. "0100" stands for version 1.00) 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string LibraryVersion;

        /// <summary>
        /// [offset 0x0048]
        /// Offset to the Add-In icon data
        /// </summary>
        public uint OffsetIcon;

        /// <summary>
        /// [offset 0x004C]
        /// Offset to the Add-In list icon data
        /// </summary>
        public uint OffsetLIcon;

        /// <summary>
        /// [offset 0x0100]
        /// Comment to the Add-In
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string UserComment;
    }
}