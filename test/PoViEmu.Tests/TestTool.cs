using System;
using PoViEmu.Common;
using Xunit;
using Xunit.Sdk;

namespace PoViEmu.Tests
{
    public static class TestTool
    {
        public static void Equal(string expected, string actual)
        {
            try
            {
                Assert.Equal(expected, actual);
            }
            catch (EqualException ex)
            {
                var text = actual.RemoveSpaces();
                throw new InvalidOperationException(text, ex);
            }
        }

        public static void Equal(byte[] expected, byte[] actual)
        {
            Assert.Equal(expected.ToHex(), actual.ToHex());
        }
    }
}