using System;
using System.IO;
using System.Linq;
using PoViEmu.Common;
using PoViEmu.Core.Hardware;
using System;
using System.IO;
using PoViEmu.Common;
using PoViEmu.Core.Hardware;
using System.Text;
using PoViEmu.Common;
using PoViEmu.Core.Hardware.Errors;
using System;
using System.IO;
using System.Linq;
using PoViEmu.Core.Hardware;
using static PoViEmu.Common.FileHelper;
using static PoViEmu.CpuFan.StateTool;

namespace PoViEmu.Tests
{
    public static class CompatCheck
    {
        public static void DoShouldRead(string dir, string fileName)
        {
            var comFile = Path.Combine(dir, $"{fileName}.com");
            var txtFile = Path.Combine(dir, $"{fileName}.txt");

            var comBytes = File.ReadAllBytes(comFile);
            var expected = TextHelper.ReadUtf8Lines(txtFile);

            var (@out, ret, diff) = Execute(comBytes);
            var actual = @out.ToLines();

            // TODO
            var obj = JsonHelper.ToJson(new { Return = ret, Diff = diff });
            File.WriteAllText($"{fileName}.json", obj, Encoding.UTF8);

            TestTool.Equal(expected, actual);
        }
    }
}