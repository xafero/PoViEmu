using System;
using static PoViEmu.Base.PropHelper;

namespace PoViEmu.SH3.CPU
{
    public static class MachParser
    {
        public static object GetByString(this MachineState m, string? name)
        {
            var (key, arg) = SplitPropName(name);
            return key switch
            {
                nameof(MachineState.PC) => m.PC,
                _ => throw new InvalidOperationException(name)
            };
        }

        public static void SetByString(this MachineState m, string name, object value)
        {
            var (key, arg) = SplitPropName(name);
            switch (key?.ToLowerInvariant())
            {
                default: throw new InvalidOperationException(name);
            }
        }
    }
}