using System.Collections.Generic;
using System.Linq;
using PoViEmu.Common.Diffs;
using System;
using PoViEmu.Core.Decoding.Ops;
using PoViEmu.Core.Hardware;
using Xunit;
using M = PoViEmu.Common.Diffs.Mode;

namespace PoViEmu.Tests
{
    public static class TestHelper
    {
        public static IEnumerable<Changed> Compare(IList<string> a, IList<string> b, bool noMem = true)
        {
            var actualC = a.Compare(b);
            if (noMem) actualC = actualC.Where(x => x.Mod != M.Memory);
            return actualC;
        }

        public static void Check(IList<Changed> actual, Changed[] expected)
        {
            var expectedL = expected.ToArray();
            var actualL = actual.ToArray();
            Assert.Equal(expectedL, actualL);
        }

        public static void EqualHex(long expected, long actual, int numbers)
        {
            switch (numbers)
            {
                case 2:
                    Assert.Equal($"{expected:X2}", $"{actual:X2}");
                    return;
                case 4:
                    Assert.Equal($"{expected:X4}", $"{actual:X4}");
                    return;
                case 5:
                    Assert.Equal($"{expected:X5}", $"{actual:X5}");
                    return;
                default:
                    throw new InvalidOperationException($"{numbers} ?!");
            }
        }

        public static IList<Changed> ExecAndCollect(this MachineState m, int count)
        {
            var res = new List<Changed>();
            var cpu = new NC3022();
            foreach (var instruct in m.Prefetch().Take(count))
            {
                var last = m.ToAllStrings();
                cpu.Execute(instruct, m);
                var now = m.ToAllStrings();
                res.AddRange(Compare(last, now));
            }
            return res;
        }
    }
}