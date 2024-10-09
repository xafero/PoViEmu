using Iced.Intel;
using PoViEmu.Core.Decoding.Ops.Consts;

namespace PoViEmu.Core.Hardware
{
    public static class Jumping
    {
        public static void SetIp(this MachineState m, Instruction instruct)
        {
            var next = (ushort)instruct.NextIP;
            m.IP = next;
        }

        public static void SetIp(this MachineState m, Instruction instruct, U16Operand jump)
        {
            var next = (ushort)instruct.NextIP;
            next = (ushort)(next + jump.Val);
            m.IP = next;
        }
    }
}