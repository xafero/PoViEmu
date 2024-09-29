using System;
using System.IO;
using PoViEmu.Common;
using PoViEmu.Core.Addins;
using PoViEmu.Core.Data;
using PoViEmu.Core.Dumps;
using PoViEmu.Core.Modules;

// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Mimes
{
    public static class MimeTyping
    {
        private static readonly byte[] V30PvApp =
        [
            0x00, 0xFF, 0x43, 0x41, 0x53, 0x49, 0x4F, 0x03
        ];

        private static readonly byte[] V30PvDump =
        [
            0x45, 0xBA, 0x43, 0x41, 0x53, 0x49, 0x4F, 0x03
        ];

        private static readonly byte[][] V30PvIcon =
        [
            [0x0C, 0x00, 0x0C, 0x00],
            [0x78, 0x00, 0x78, 0x00]
        ];

        private static readonly byte[] V30PvChip =
        [
            0xB8, 0x00, 0xF0, 0x8E, 0xD0, 0xBC, 0x00, 0x01, 0xEB, 0x00, 0xB9
        ];

        private static readonly byte[] SH3PvApp =
        [
            0x50, 0x56, 0x41, 0x50, 0x4C, 0x48, 0x45, 0x44, 0x56, 0x32, 0x30
        ];

        private static readonly byte[] SH3PvDump =
        [
            0x43, 0x41, 0x53, 0x49, 0x4F, 0x50, 0x56, 0x4F, 0x53, 0x32, 0x30, 0x30, 0x55
        ];

        private static readonly byte[][] V30PvData =
        [
            [0x53, 0x57, 0xE6, 0xE7, 0xE7, 0xE7],
            [0x4D, 0x21]
        ];

        public enum MimeType
        {
            Unknown = 0,

            V30PvIcon,

            V30PvApp,

            V30PvDump,

            V30PvChip,

            SH3PvApp,

            SH3PvDump,

            V30PvData
        }

        public static MimeType? GetMimeType(this byte[] bytes)
        {
            if (bytes.IsContained(V30PvApp))
                return MimeType.V30PvApp;
            if (bytes.IsContained(V30PvDump))
                return MimeType.V30PvDump;
            if (bytes.IsContained(V30PvIcon[0]) || bytes.IsContained(V30PvIcon[1]))
                return MimeType.V30PvIcon;
            if (bytes.IsContained(V30PvChip))
                return MimeType.V30PvChip;
            if (bytes.IsContained(SH3PvApp))
                return MimeType.SH3PvApp;
            if (bytes.IsContained(V30PvData[0]) || bytes.IsContained(V30PvData[1]))
                return MimeType.V30PvData;
            if (bytes.IsContained(SH3PvDump))
                return MimeType.SH3PvDump;
            return null;
        }

        public static object? Load(this MimeType kind, byte[] bytes)
        {
            return kind switch
            {
                MimeType.V30PvApp => LoadV30App(bytes),
                MimeType.V30PvDump => LoadV30Dump(bytes),
                MimeType.V30PvData => LoadV30Data(bytes),
                MimeType.SH3PvApp => LoadSH3App(bytes),
                _ => new InvalidOperationException($"{kind}, {bytes.Length} B ?!")
            };
        }

        private static PvaInfo? LoadSH3App(byte[] bytes)
        {
            try
            {
                var addIn = PvaReader.Read(bytes);
                return addIn;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        private static TwfInfo? LoadV30Data(byte[] bytes)
        {
            try
            {
                var addIn = TwfReader.Read(bytes);
                return addIn;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        private static DumpInfo? LoadV30Dump(byte[] bytes)
        {
            try
            {
                var info = DumpReader.Read(bytes);
                var mem = new MemoryStream(bytes);
                info.LoadOsAddIns(mem);
                return info;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        private static AddInInfo? LoadV30App(byte[] bytes)
        {
            try
            {
                var addIn = AddInReader.Read(bytes);
                return addIn;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }
    }
}