using System;
using PoViEmu.Base;
using PoViEmu.Tests.Base;
using Xunit;
using static PoViEmu.Base.CPU.BitTool;

namespace PoViEmu.Tests.CPU
{
    public class BytesTest
    {
        [Theory]
        [InlineData(0xABCD, 0xCD, 0xAB, 0x3412, 0x12, 0x34)]
        [InlineData(0x8220, 0x20, 0x82, 0x8B7C, 0x7C, 0x8B)]
        [InlineData(0x1210, 0x10, 0x12, 0x174D, 0x4D, 0x17)]
        [InlineData(0x3741, 0x41, 0x37, 0x3A65, 0x65, 0x3A)]
        public void ShouldOverwrite(ushort r1, byte l1, byte h1, ushort r2, byte l2, byte h2)
        {
            TestHelper.EqualHex(GetLow(r1), l1, 2);
            TestHelper.EqualHex(GetHigh(r1), h1, 2);

            r1 = SetLow(r1, l2);
            r1 = SetHigh(r1, h2);

            TestHelper.EqualHex(r2, r1, 4);
        }

        [Theory]
        [InlineData("ABCD3412", EndianMode.BigEndian,
            -85, -21555, -1412615150,
            171, 43981, 2882352146)]
        [InlineData("ABCD3412", EndianMode.LittleEndian,
            -85, -12885, 305450411,
            171, 52651, 305450411)]
        [InlineData("82208B7C", EndianMode.BigEndian,
            -126, -32224, -2111796356,
            130, 33312, 2183170940)]
        [InlineData("82208B7C", EndianMode.LittleEndian,
            -126, 8322, 2089492610,
            130, 8322, 2089492610)]
        [InlineData("1210174D", EndianMode.BigEndian,
            18, 4624, 303044429,
            18, 4624, 303044429)]
        [InlineData("1210174D", EndianMode.LittleEndian,
            18, 4114, 1293357074,
            18, 4114, 1293357074)]
        [InlineData("37413A65", EndianMode.BigEndian,
            55, 14145, 927021669,
            55, 14145, 927021669)]
        [InlineData("37413A65", EndianMode.LittleEndian,
            55, 16695, 1698316599,
            55, 16695, 1698316599)]
        public void ShouldEndian(string hex, EndianMode m,
            sbyte xI8, short xI16, int xI32,
            byte xU8, ushort xU16, uint xU32)
        {
            var bytes = Convert.FromHexString(hex);

            var i8 = Endian.ReadInt8(bytes);
            var i16 = Endian.ReadInt16(bytes, mode: m);
            var i32 = Endian.ReadInt32(bytes, mode: m);

            Assert.Equal(xI8, i8);
            Assert.Equal(xI16, i16);
            Assert.Equal(xI32, i32);

            Assert.Equal(hex[..2], Convert.ToHexString(Endian.WriteInt8(i8)));
            Assert.Equal(hex[..4], Convert.ToHexString(Endian.WriteInt16(i16, m)));
            Assert.Equal(hex, Convert.ToHexString(Endian.WriteInt32(i32, m)));

            var u8 = Endian.ReadUInt8(bytes);
            var u16 = Endian.ReadUInt16(bytes, mode: m);
            var u32 = Endian.ReadUInt32(bytes, mode: m);

            Assert.Equal(xU8, u8);
            Assert.Equal(xU16, u16);
            Assert.Equal(xU32, u32);

            Assert.Equal(hex[..2], Convert.ToHexString(Endian.WriteUInt8(u8)));
            Assert.Equal(hex[..4], Convert.ToHexString(Endian.WriteUInt16(u16, m)));
            Assert.Equal(hex, Convert.ToHexString(Endian.WriteUInt32(u32, m)));
        }
    }
}