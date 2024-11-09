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

            string[] actual = ["?"];

            TestTool.Equal(expected, actual);
        }

        private static void Execute()
        {
            var state = new MachineState();
            var l = state.Collect();
            l.PropertyChanged += (s, e) =>
            {
                var txt = $" | {e.PropertyName} " +
                          $"| {e.Old.Format()} " +
                          $"| {e.New.Format()}";
                throw new InvalidOperationException(txt);
            };
            
            
        }
    }
}