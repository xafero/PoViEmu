using System;
using System.IO;
using System.Text;
using PoViEmu.Core;
using PoViEmu.Common;
using Xunit;

namespace PoViEmu.Tests
{
    public class AddInTest
    {
        [Theory]
        [InlineData("Small")]
        public void ShouldRead(string fileName)
        {
            var dir = Path.Combine("Resources", "Addins");
            DoShouldRead(dir, fileName);
        }

        public static void DoShouldRead(string dir, string fileName)
        {
            var file = Path.Combine(dir, $"{fileName}.bin");
            using var stream = File.OpenRead(file);
            var addIn = AddInReader.Read(stream);

            var actual = JsonHelper.ToJson(addIn);
            var jFile = Path.Combine(dir, $"{fileName}.json");
            var expected = TextHelper.ToText(jFile);
            TestTool.Equal(expected, actual);
        }
    }
}
