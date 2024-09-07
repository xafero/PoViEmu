using System.Linq;

namespace PoViEmu.Core.Decoding
{
    public static class StateExt
    {
        public const int StackSize = 2;

        public static void PushStack(this MachineState state, ushort value)
        {
            state.SP -= StackSize;

            var list = state.Stack.First();
            list.Value.Insert(0, value);
        }
    }
}