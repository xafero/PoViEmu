using System.Runtime.InteropServices;

namespace PoViEmu.Core.Modules
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PvaHeader
    {
        /// <summary>
        /// 0x0000 PVA signature (e.g. "PVAPLHEDV20")
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public char[] Signature;

        /// <summary>
        /// 0x000C Version (e.g. 00 01 00 00 for version 1.0)
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] HeaderVersion;

        /// <summary>
        /// 0x0010 File type (e.g. 00 00 00 00)
        /// </summary>
        public uint FileType;

        /// <summary>
        /// 0x0014 Reserved size (e.g. 00 00 00 00)
        /// </summary>
        public uint ReservedSize;

        /// <summary>
        /// 0x0018 Keycodes (unknown, 32 bytes)
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] Keycodes;

        /// <summary>
        /// 0x0038 Segment number (e.g. 00 00 00 01)
        /// </summary>
        public uint SegmentNumber;

        /// <summary>
        /// 0x003C Offset to ELF body (e.g. 00 00 01 A0)
        /// </summary>
        public uint ElfOffset;

        /// <summary>
        /// 0x0040 PVA mode (e.g. 00 01 for PV, CP, PV/CP)
        /// </summary>
        public ushort PvaMode;

        /// <summary>
        /// 0x0042 PVA type (e.g. 00 00 for Application)
        /// </summary>
        public ushort PvaType;

        /// <summary>
        /// 0x0044 Offset to Program info header (e.g. 00 00 00 48)
        /// </summary>
        public uint ProgramInfoOffset;

        /// <summary>
        /// 0x0048 Program name (e.g. "TextViewer")
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string ProgramName;

        /// <summary>
        /// 0x0088 Program version (e.g. 01 25)
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public char[] ProgramVersion;

        /// <summary>
        /// 0x008A Library version (e.g. 01 00)
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public char[] LibraryVersion;

        /// <summary>
        /// 0x008C ELF length (e.g. 00 01 4A 08)
        /// </summary>
        public uint ElfLength;

        /// <summary>
        /// 0x0090 Year of creation (e.g. 20 02)
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] CreationYear;

        /// <summary>
        /// 0x0092 Month of creation (e.g. 12)
        /// </summary>
        public byte CreationMonth;

        /// <summary>
        /// 0x0093 Day of creation (e.g. 11)
        /// </summary>
        public byte CreationDay;

        /// <summary>
        /// 0x0094 Hour of creation (e.g. 15)
        /// </summary>
        public byte CreationHour;

        /// <summary>
        /// 0095h Minute of creation (e.g. 59)
        /// </summary>
        public byte CreationMinute;

        /// <summary>
        /// 0x0096 Second of creation (e.g. 02)
        /// </summary>
        public byte CreationSecond;

        /// <summary>
        /// 0x0097 Dummy byte (e.g. 00)
        /// </summary>
        public byte Dummy;

        /// <summary>
        /// 0x0098 Application code (e.g. 00 01)
        /// </summary>
        public ushort ApplicationCode;

        /// <summary>
        /// 0x009A Icon height (e.g. 00 1C for 28)
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] IconHeight;

        /// <summary>
        /// 0x009C Icon width (e.g. 00 2D for 45)
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] IconWidth;

        /// <summary>
        /// 0x009E Icon type (e.g. 00 00)
        /// </summary>
        public ushort IconType;

        /// <summary>
        /// 0x00A0 Small icon height (e.g. 00 14 for 20)
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] SmallIconHeight;

        /// <summary>
        /// 0x00A2 Small icon width (e.g. 00 1B for 27)
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] SmallIconWidth;

        /// <summary>
        /// 0x00A4 Small icon type (e.g. 00 00)
        /// </summary>
        public ushort SmallIconType;

        /// <summary>
        /// 0x00A6 Unknown field (e.g. 00 00)
        /// </summary>
        public ushort UnknownField;
    }
}