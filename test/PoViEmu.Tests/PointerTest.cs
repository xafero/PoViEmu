using System.Linq;
using Iced.Intel;
using PoViEmu.Common;
using PoViEmu.Core.Decoding;
using PoViEmu.Core.Decoding.Ops;
using PoViEmu.Core.Decoding.Ops.Mems;
using PoViEmu.Core.Hardware;
using Xunit;

namespace PoViEmu.Tests
{
    public class PointerTest
    {
        [Theory]
        // #5
        [InlineData("mov al,[bx+si+10h]", "Byte [DS:BX+SI+16]", 0x8A4010, "DS:2130")]
        [InlineData("mov ch,[bp+di+125h]", "Byte [SS:BP+DI+293]", 0x8AAB2501, "SS:112A")]
        [InlineData("mov bx,cs:[bx+di+2]", "UInt16 [CS:BX+DI+2]", 0x2E8B5902, "CS:2007")]
        [InlineData("mov al,[bx-1]", "Byte [DS:BX-1]", 0x8A87FFFF, "DS:000F", 0x10)]
        // #4
        [InlineData("mov ds:[0E818h],ax", "UInt16 [DS:e818]", 0xA318E8, "DS:E818")]
        [InlineData("mov es:[0F86Ah],ax", "UInt16 [ES:f86a]", 0x26A36AF8, "ES:F86A")]
        // #3
        [InlineData("mov [di+3FDDh],bx", "UInt16 [DS:DI+16349]", 0x899DDD3F, "DS:3FE2")]
        [InlineData("add bh,[bx-2Eh]", "Byte [DS:BX-46]", 0x027FD2, "DS:1FD2")]
        [InlineData("mov sp,[si]", "UInt16 [DS:SI]", 0x8B24, "DS:0120")]
        // #2
        [InlineData("mov [bp-5],ax", "UInt16 [SS:BP-5]", 0x8946FB, "SS:0FFB")]
        [InlineData("mov [bp-4],ax", "UInt16 [SS:BP-4]", 0x8946FC, "SS:0FFC")]
        [InlineData("mov [bp-3],ax", "UInt16 [SS:BP-3]", 0x8946FD, "SS:0FFD")]
        [InlineData("mov [bp-2],ax", "UInt16 [SS:BP-2]", 0x8946FE, "SS:0FFE")]
        [InlineData("mov [bp-1],ax", "UInt16 [SS:BP-1]", 0x8946FF, "SS:0FFF")]
        [InlineData("mov [bp],ax", "UInt16 [SS:BP]", 0x894600, "SS:1000")]
        [InlineData("mov [bp+1],ax", "UInt16 [SS:BP+1]", 0x894601, "SS:1001")]
        [InlineData("mov [bp+2],ax", "UInt16 [SS:BP+2]", 0x894602, "SS:1002")]
        [InlineData("mov [bp+3],ax", "UInt16 [SS:BP+3]", 0x894603, "SS:1003")]
        [InlineData("mov [bp+4],ax", "UInt16 [SS:BP+4]", 0x894604, "SS:1004")]
        [InlineData("mov [bp+5],ax", "UInt16 [SS:BP+5]", 0x894605, "SS:1005")]
        // #1
        [InlineData("sub [bx+di-5],si", "UInt16 [DS:BX+DI-5]", 0x2971FB, "DS:2000")]
        [InlineData("sub [bx+di-4],si", "UInt16 [DS:BX+DI-4]", 0x2971FC, "DS:2001")]
        [InlineData("sub [bx+di-3],si", "UInt16 [DS:BX+DI-3]", 0x2971FD, "DS:2002")]
        [InlineData("sub [bx+di-2],si", "UInt16 [DS:BX+DI-2]", 0x2971FE, "DS:2003")]
        [InlineData("sub [bx+di-1],si", "UInt16 [DS:BX+DI-1]", 0x2971FF, "DS:2004")]
        [InlineData("sub [bx+di],si", "UInt16 [DS:BX+DI]", 0x297100, "DS:2005")]
        [InlineData("sub [bx+di+1],si", "UInt16 [DS:BX+DI+1]", 0x297101, "DS:2006")]
        [InlineData("sub [bx+di+2],si", "UInt16 [DS:BX+DI+2]", 0x297102, "DS:2007")]
        [InlineData("sub [bx+di+3],si", "UInt16 [DS:BX+DI+3]", 0x297103, "DS:2008")]
        [InlineData("sub [bx+di+4],si", "UInt16 [DS:BX+DI+4]", 0x297104, "DS:2009")]
        [InlineData("sub [bx+di+5],si", "UInt16 [DS:BX+DI+5]", 0x297105, "DS:200A")]
        public void ShouldCalcPointer(string code, string txt, long raw, string dst, int? bx = null)
        {
            var b = BytesHelper.ToArray(raw);
            var d = new ByteArrayCodeReader(b).Create16(0x100);
            var i = d.FirstOrDefault();
            var o = i.GetOps().OfType<MemOperand>().First();

            var expected = $"{txt}";
            var actual = $"{o}";
            Assert.Equal(expected, actual);
            Assert.Equal(code, i.ToString());

            var m = GenerateState();
            m.BX = bx is { } bxv ? (ushort)bxv : m.BX;
            var target = $"{o.Seg}:{o.OffA(m):X4}";
            Assert.Equal(dst, target);
        }

        private static MachineState GenerateState(ushort bp = 0x1000, ushort bx = 0x2000,
            ushort si = 0x0120, ushort di = 0x0005)
        {
            var state = new MachineState
            {
                BP = bp, BX = bx, SI = si, DI = di
            };
            return state;
        }
    }
}