using System;
using PoViEmu.Base;
using Xunit;
using Xunit.Sdk;

namespace PoViEmu.Tests.Base
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

        public static void Equal(string[] expected, string[] actual)
        {
            var nl = Environment.NewLine;
            var expectedTxt = string.Join(nl, expected);
            var actualTxt = string.Join(nl, actual);
            Equal(expectedTxt, actualTxt);
        }
    }
}