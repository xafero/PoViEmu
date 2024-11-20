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
        [InlineData("U16 [SS BP -5]", 0x8946FB)]
        [InlineData("U16 [SS BP -4]", 0x8946FC)]
        [InlineData("U16 [SS BP -3]", 0x8946FD)]
        [InlineData("U16 [SS BP -2]", 0x8946FE)]
        [InlineData("U16 [SS BP -1]", 0x8946FF)]
        [InlineData("U16 [SS BP 0]", 0x894600)]
        [InlineData("U16 [SS BP 1]", 0x894601)]
        [InlineData("U16 [SS BP 2]", 0x894602)]
        [InlineData("U16 [SS BP 3]", 0x894603)]
        [InlineData("U16 [SS BP 4]", 0x894604)]
        [InlineData("U16 [SS BP 5]", 0x894605)]
        public void ShouldCalcPointer(string txt, int raw)
        {
            var b = BytesHelper.ToArray(raw);
            var d = new ByteArrayCodeReader(b).Create16(0x100);
            var i = d.Single();
            var fo = i.GetOps().First();
            var o = (Mu16Operand)fo;

            var expected = $"{txt}";
            var actual = $"{o}";
            Assert.Equal(expected, actual);
        }
    }
}