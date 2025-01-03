using System;
using Xunit;

namespace PoViEmu.Tests.Base
{
    public static class TestHelper
    {
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
    }
}