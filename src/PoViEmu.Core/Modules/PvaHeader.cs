using System.Runtime.InteropServices;

namespace PoViEmu.Core.Modules
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PvaHeader
    {
        /// <summary>
        /// 0000h PVA signature (e.g. "PVAPLHEDV20")
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        public char[] Signature;

        /// <summary>
        /// 000Ch Version (e.g. 00010000h for version 1.0)
        /// </summary>
        public ushort VersionMajor;

        public ushort VersionMinor;

        /// <summary>
        /// 0010h File type (e.g. 00000000h)
        /// </summary>
        public uint FileType;

        /// <summary>
        /// 0014h Reserved size (e.g. 00000000h)
        /// </summary>
        public uint ReservedSize;

        /// <summary>
        /// 0018h Keycodes (unknown, 32 bytes)
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] Keycodes;

        /// <summary>
        /// 0038h Segment number (e.g. 00000001h)
        /// </summary>
        public uint SegmentNumber;

        /// <summary>
        /// 003Ch Offset to ELF body (e.g. 000001A0h)
        /// </summary>
        public uint ElfOffset;

        /// <summary>
        /// 0040h PVA mode (e.g. 0001h for PV, CP, PV/CP)
        /// </summary>
        public ushort PvaMode;

        /// <summary>
        /// 0042h PVA type (e.g. 0000h for Application)
        /// </summary>
        public ushort PvaType;

        /// <summary>
        /// 0044h Offset to Program info header (e.g. 00000048h)
        /// </summary>
        public uint ProgramInfoOffset;

        /// <summary>
        /// 0048h Program name (e.g. "TextViewer")
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string ProgramName;

        /// <summary>
        /// 0088h Program version (e.g. 0125h)
        /// </summary>
        public ushort ProgramVersionMajor;

        public ushort ProgramVersionMinor;

        /// <summary>
        /// 008Ah Library version (e.g. 0100h)
        /// </summary>
        public ushort libraryVersionMajor;

        public ushort libraryVersionMinor;

        /// <summary>
        /// 008Ch ELF length (e.g. 00014A08h)
        /// </summary>
        public uint ElfLength;

        /// <summary>
        /// 0090h Year of creation (e.g. 2002)
        /// </summary>
        public ushort CreationYear;

        /// <summary>
        /// 0092h Month of creation (e.g. 12)
        /// </summary>
        public byte CreationMonth;

        /// <summary>
        /// 0093h Day of creation (e.g. 11)
        /// </summary>
        public byte CreationDay;

        /// <summary>
        /// 0094h Hour of creation (e.g. 15)
        /// </summary>
        public byte CreationHour;

        /// <summary>
        /// 0095h Minute of creation (e.g. 59)
        /// </summary>
        public byte CreationMinute;

        /// <summary>
        /// 0096h Second of creation (e.g. 02)
        /// </summary>
        public byte CreationSecond;

        /// <summary>
        /// 0097h Dummy byte (e.g. 00)
        /// </summary>
        public byte Dummy;

        /// <summary>
        /// 0098h Application code (e.g. 0001h)
        /// </summary>
        public ushort ApplicationCode;

        /// <summary>
        /// 009Ah Icon height (e.g. 001Ch for 28)
        /// </summary>
        public ushort IconHeight;

        /// <summary>
        /// 009Ch Icon width (e.g. 002Dh for 45)
        /// </summary>
        public ushort IconWidth;

        /// <summary>
        /// 009Eh Icon type (e.g. 0000h)
        /// </summary>
        public ushort IconType;

        /// <summary>
        /// 00A0h Small icon height (e.g. 0014h for 20)
        /// </summary>
        public ushort SmallIconHeight;

        /// <summary>
        /// 00A2h Small icon width (e.g. 001Bh for 27)
        /// </summary>
        public ushort SmallIconWidth;

        /// <summary>
        /// 00A4h Small icon type (e.g. 0000h)
        /// </summary>
        public ushort SmallIconType;

        /// <summary>
        /// 00A6h Unknown field (e.g. 0000h)
        /// </summary>
        public ushort UnknownField;
    }
}