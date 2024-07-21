using System.Runtime.InteropServices;

namespace PoViEmu.Core
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DumpHeader
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] Signature;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string Model;
    }
}