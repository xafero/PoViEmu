using System;
using System.Linq;
using Iced.Intel;
using PoViEmu.Common;
using PoViEmu.Core.Decoding;
using PoViEmu.Core.Decoding.Ops;
using Xunit;

namespace PoViEmu.Tests
{
    public class PointerTest
    {
        [Theory]
        // #4
        [InlineData("?", "?", 0xA318E8)]
        [InlineData("?", "?", 0x26A36AF8)]
        // #3
        [InlineData("mov [di+3FDDh],bx", "U16 [DS:DI+16349]", 0x899DDD3F)]
        [InlineData("add bh,[bx-2Eh]", "U16 [DS:BX-46]", 0x027FD2)]
        [InlineData("mov sp,[si]", "U16 [DS:SI+0]", 0x8B24)]
        // #2
        [InlineData("mov [bp-5],ax", "U16 [SS:BP-5]", 0x8946FB)]
        [InlineData("mov [bp-4],ax", "U16 [SS:BP-4]", 0x8946FC)]
        [InlineData("mov [bp-3],ax", "U16 [SS:BP-3]", 0x8946FD)]
        [InlineData("mov [bp-2],ax", "U16 [SS:BP-2]", 0x8946FE)]
        [InlineData("mov [bp-1],ax", "U16 [SS:BP-1]", 0x8946FF)]
        [InlineData("mov [bp],ax", "U16 [SS:BP+0]", 0x894600)]
        [InlineData("mov [bp+1],ax", "U16 [SS:BP+1]", 0x894601)]
        [InlineData("mov [bp+2],ax", "U16 [SS:BP+2]", 0x894602)]
        [InlineData("mov [bp+3],ax", "U16 [SS:BP+3]", 0x894603)]
        [InlineData("mov [bp+4],ax", "U16 [SS:BP+4]", 0x894604)]
        [InlineData("mov [bp+5],ax", "U16 [SS:BP+5]", 0x894605)]
        // #1
        [InlineData("sub [bx+di-5],si", "U16 [DS:BX+DI-5]", 0x2971FB)]
        [InlineData("sub [bx+di-4],si", "U16 [DS:BX+DI-4]", 0x2971FC)]
        [InlineData("sub [bx+di-3],si", "U16 [DS:BX+DI-3]", 0x2971FD)]
        [InlineData("sub [bx+di-2],si", "U16 [DS:BX+DI-2]", 0x2971FE)]
        [InlineData("sub [bx+di-1],si", "U16 [DS:BX+DI-1]", 0x2971FF)]
        [InlineData("sub [bx+di],si", "U16 [DS:BX+DI+0]", 0x297100)]
        [InlineData("sub [bx+di+1],si", "U16 [DS:BX+DI+1]", 0x297101)]
        [InlineData("sub [bx+di+2],si", "U16 [DS:BX+DI+2]", 0x297102)]
        [InlineData("sub [bx+di+3],si", "U16 [DS:BX+DI+3]", 0x297103)]
        [InlineData("sub [bx+di+4],si", "U16 [DS:BX+DI+4]", 0x297104)]
        [InlineData("sub [bx+di+5],si", "U16 [DS:BX+DI+5]", 0x297105)]
        public void ShouldCalcPointer(string code, string txt, long raw)
        {
            var b = BytesHelper.ToArray(raw);
            var d = new ByteArrayCodeReader(b).Create16(0x100);
            var i = d.FirstOrDefault();
            var fo = i.GetOps().OfType<Mu16Operand>().First();
            var o = fo;

            var expected = $"{txt}";
            var actual = $"{o}";
            Assert.Equal(expected, actual);
            Assert.Equal(code, i.ToString());
        }
    }
}