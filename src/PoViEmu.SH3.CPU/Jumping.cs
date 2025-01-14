using PoViEmu.SH3.ISA;
using PoViEmu.SH3.ISA.Ops.Mems;
using PoViEmu.SH3.ISA.Ops.Regs;

namespace PoViEmu.SH3.CPU
{
    internal static class Jumping
    {
        public static void BranchIfFalse(this MachineState s, int d, ref uint nextIP)
        {
            long disp;
            if ((d & 0x80) == 0)
                disp = 0x000000FF & d;
            else
                disp = 0xFFFFFF00 | d;
            if (s.T == false)
                nextIP = (uint)(s.PC + (disp << 1) + 4);
            else
                nextIP = s.PC + 2;
        }

        public static void BranchIfFalseDelay(this MachineState s, int d, ref uint nextIP)
        {
            int disp;
            var temp = s.PC;
            if ((d & 0x80) == 0)
                disp = 0x000000FF & d;
            else
                disp = (int)(0xFFFFFF00 | d);
            if (s.T == false)
            {
                nextIP = (uint)(s.PC + (disp << 1) + 4);
                DelaySlot(temp + 2);
            }
            else
                nextIP = s.PC + 2;
        }

        private static void DelaySlot(uint temp)
        {
            // TODO delay slot ?!
        }

        public static void Branch(this MachineState s, int d, ref uint nextIP)
        {
            int disp;
            if ((d & 0x800) == 0)
                disp = 0x00000FFF & d;
            else
                disp = (int)(0xFFFFF000 | d);
            var temp = nextIP;
            nextIP = (uint)(s.PC + (disp << 1) + 4);
            DelaySlot(temp + 2);
        }

        public static void BranchFar(this MachineState s, Reg32Operand m, ref uint nextIP)
        {
            var temp = nextIP;
            nextIP = s.PC + s[m];
            DelaySlot(temp + 2);
        }

        public static void BranchSubroutine(this MachineState s, int d, ref uint nextIP)
        {
            long disp;
            if ((d & 0x800) == 0)
                disp = 0x00000FFF & d;
            else
                disp = 0xFFFFF000 | d;
            s.PR = nextIP;
            nextIP = (uint)(s.PC + (disp << 1) + 4);
            DelaySlot(s.PR + 2);
        }

        public static void BranchSubroutineFar(this MachineState s, Reg32Operand m, ref uint nextIP)
        {
            s.PR = s.PC;
            nextIP = s.PC + s[m];
            DelaySlot(s.PR + 2);
        }

        public static void BranchIfTrue(this MachineState s, int d, ref uint nextIP)
        {
            int disp;
            if ((d & 0x80) == 0)
                disp = 0x000000FF & d;
            else
                disp = (int)(0xFFFFFF00 | d);
            if (s.T)
                nextIP = (uint)(s.PC + (disp << 1) + 4);
            else
                nextIP = s.PC + 2;
        }

        public static void BranchIfTrueDelay(this MachineState s, int d, ref uint nextIP)
        {
            int disp;
            var temp = s.PC;
            if ((d & 0x80) == 0)
                disp = 0x000000FF & d;
            else
                disp = (int)(0xFFFFFF00 | d);
            if (s.T)
            {
                nextIP = (uint)(s.PC + (disp << 1) + 4);
                DelaySlot(temp + 2);
            }
            else
            {
                nextIP = s.PC + 2;
            }
        }

        public static void Jump(this MachineState s, Reg32Operand m, ref uint nextIP)
        {
            var temp = s.PC;
            nextIP = s[m] + 4;
            DelaySlot(temp + 2);
        }

        public static void JumpSubroutine(this MachineState s, Mu32Operand mem, ref uint nextIP)
        {
            s.PR = s.PC;
            nextIP = mem[s] + 4;
            DelaySlot(s.PR + 2);
        }

        public static void ReturnFromEx(this MachineState s, ref uint nextIP)
        {
            var temp = s.PC;
            nextIP = s.SPC;
            s.SR = (Flagged)s.SSR;
            DelaySlot(temp + 2);
        }

        public static void ReturnSubroutine(this MachineState s, ref uint nextIP)
        {
            var temp = s.PC;
            nextIP = s.PR + 4;
            DelaySlot(temp + 2);
        }
    }
}