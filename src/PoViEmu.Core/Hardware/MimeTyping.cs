using System;
using PoViEmu.Common;
using PoViEmu.Core.Addins;

namespace PoViEmu.Core.Hardware
{
    public static class MimeTyping
    {
        private static readonly byte[] X86PvApp =
        [
            0x00, 0xFF, 0x43, 0x41, 0x53, 0x49, 0x4F, 0x03
        ];

        private static readonly byte[] X86PvDump =
        [
            0x45, 0xBA, 0x43, 0x41, 0x53, 0x49, 0x4F, 0x03
        ];

        private static readonly byte[][] X86PvIcon =
        [
            [0x0C, 0x00, 0x0C, 0x00],
            [0x78, 0x00, 0x78, 0x00]
        ];

        private static readonly byte[] X86PvChip =
        [
            0xB8, 0x00, 0xF0, 0x8E, 0xD0, 0xBC, 0x00, 0x01, 0xEB, 0x00, 0xB9
        ];

        public enum MimeType
        {
            Unknown = 0,

            X86PvIcon,

            X86PvApp,

            X86PvDump,

            X86PvChip
        }

        public static MimeType? GetMimeType(this byte[] bytes)
        {
            if (bytes.IsContained(X86PvApp))
                return MimeType.X86PvApp;
            if (bytes.IsContained(X86PvDump))
                return MimeType.X86PvDump;
            if (bytes.IsContained(X86PvIcon[0]) || bytes.IsContained(X86PvIcon[1]))
                return MimeType.X86PvIcon;
            if (bytes.IsContained(X86PvChip))
                return MimeType.X86PvChip;
            return null;
        }

        public static object Load(this MimeType kind, byte[] bytes)
        {
            return kind switch
            {
                MimeType.X86PvApp => LoadApp(bytes),
                _ => new InvalidOperationException($"{kind}, {bytes.Length} B ?!")
            };
        }

        private static AddInInfo LoadApp(byte[] bytes)
        {
            var addIn = AddInReader.Read(bytes);
            return addIn;
        }
    }
}