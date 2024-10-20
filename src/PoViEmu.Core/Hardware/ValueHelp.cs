using System;

namespace Hallo
{
    public static class ValueHelp
    {
        public static string Format(this object obj)
        {
            return obj switch
            {
                bool b => b ? "1" : "0",
                ushort us => $"0x{us:X4}",
                _ => throw new InvalidOperationException($"[{obj.GetType()}] {obj}")
            };
        }
    }
}