using System.IO;
using System.Linq;
using System.Text;
using PoViEmu.Base;
using PoViEmu.Tests.ISA.Util;
using Xunit;
using static PoViEmu.Tests.ISA.Util.DecodeHelp;

namespace PoViEmu.Tests.ISA
{
    public class DecodingTest
    {
        [Theory]
        [InlineData("sh3big")]
        public void TestSh3(string fileName)
        {
            var dir = Path.Combine("Resources", "SH3");
            var jsonFile = Path.Combine(dir, $"{fileName}.json");

            const string pe = "parsed.e.txt";
            ParseAll(new JsonParser(jsonFile), File.CreateText(pe), ToLine);

            const string pa = "parsed.a.txt";
            ParseAll(new MyParser(), File.CreateText(pa), ToLine);

            var peLines = TextHelper.ReadUtf8Lines(pe);
            var paLines = TextHelper.ReadUtf8Lines(pa);
            Assert.Equal(peLines.Length, paLines.Length);

            var missing = peLines.Except(paLines).ToArray();
            File.WriteAllLines("parsed.m.txt", missing, Encoding.UTF8);
            Assert.Equal(peLines, paLines);
            Assert.Empty(missing);
        }
    }
}