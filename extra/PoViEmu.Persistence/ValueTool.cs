using System;

namespace PoViEmu.Workbook
{
    public static class ValueTool
    {
        public static T FromStr<T>(string text)
        {
            var type = typeof(T);
            throw new InvalidOperationException($"{type} : {text}");
        }
    }
}