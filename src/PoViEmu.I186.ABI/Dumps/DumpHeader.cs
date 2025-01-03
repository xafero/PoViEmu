using System.Runtime.InteropServices;

namespace PoViEmu.I186.ABI.Dumps
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DumpHeader
    {
        /// <summary>
        /// [offset 0x0000]
        /// Signature of the Add-In
        /// {0x45, 0xBA, 'C','A','S','I','O', 0x03}
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] Signature;

        /// <summary>
        /// [offset 0x0008]
        /// Model identifier
        /// {'Z','4','8','6'}
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string Model;
    }
}