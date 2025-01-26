using PoViEmu.SH3.ISA;
using PoViEmu.SH3.ISA.Ops.Mems;
using PoViEmu.SH3.ISA.Ops.Places;
using PoViEmu.SH3.ISA.Ops.Regs;

// ReSharper disable InconsistentNaming

namespace PoViEmu.SH3.CPU
{
    internal static class Jumping
    {
        public static void BranchIfFalse(this MachineState s, AddressOperand d, ref uint nextIP)
        {
            // TODO
            //long disp;
            //if ((d & 0x80) == 0)
            //    disp = 0x000000FF & d;
            //else
            //    disp = 0xFFFFFF00 | d;
            // nextIP = (uint)(s.PC + (disp << 1) + 4);

            if (s.T == false)
                nextIP = (uint)(d.CalcAddr(s));
            else
                nextIP = s.PC + 2;
        }

        public static void BranchIfFalseDelay(this MachineState s, AddressOperand d, ref uint nextIP,
            ref uint? delaySlot)
        {
            // TODO
            //int disp;
            //if ((d & 0x80) == 0)
            //    disp = 0x000000FF & d;
            //else
            //    disp = (int)(0xFFFFFF00 | d);
            // nextIP = (uint)(s.PC + (disp << 1) + 4);

            var temp = s.PC;
            if (s.T == false)
            {
                nextIP = (uint)(d.CalcAddr(s));
                delaySlot = (temp + 2);
            }
            else
                nextIP = s.PC + 2;
        }

        public static void Branch(this MachineState s, AddressOperand d, ref uint nextIP,
            ref uint? delaySlot)
        {
            // TODO
            // int disp;
            // if ((d & 0x800) == 0)
            //      disp = 0x00000FFF & d;
            // else
            //      disp = (int)(0xFFFFF000 | d);
            // nextIP = (uint)(s.PC + (disp << 1) + 4);

            var temp = s.PC;
            nextIP = (uint)(d.CalcAddr(s));
            delaySlot = (temp + 2);
        }

        public static void BranchFar(this MachineState s, Reg32Operand m, ref uint nextIP,
            ref uint? delaySlot)
        {
            var temp = s.PC;
            nextIP = s.PC + s[m];
            delaySlot = (temp + 2);
        }

        public static void BranchSubroutine(this MachineState s, AddressOperand d, ref uint nextIP,
            ref uint? delaySlot)
        {
            // TODO
            // long disp;
            // if ((d & 0x800) == 0)
            //      disp = 0x00000FFF & d;
            // else
            //      disp = 0xFFFFF000 | d;
            // nextIP = (uint)(s.PC + (disp << 1) + 4);

            s.PR = s.PC;
            nextIP = (uint)(d.CalcAddr(s));
            delaySlot = (s.PR + 2);
        }

        public static void BranchSubroutineFar(this MachineState s, Reg32Operand m, ref uint nextIP,
            ref uint? delaySlot)
        {
            s.PR = s.PC;
            nextIP = s.PC + s[m];
            delaySlot = (s.PR + 2);
        }

        public static void BranchIfTrue(this MachineState s, AddressOperand d, ref uint nextIP)
        {
            // TODO
            // int disp;
            // if ((d & 0x80) == 0)
            //      disp = 0x000000FF & d;
            // else
            //      disp = (int)(0xFFFFFF00 | d);
            // nextIP = (uint)(s.PC + (disp << 1) + 4);

            if (s.T)
                nextIP = (uint)(d.CalcAddr(s));
            else
                nextIP = s.PC + 2;
        }

        public static void BranchIfTrueDelay(this MachineState s, AddressOperand d, ref uint nextIP,
            ref uint? delaySlot)
        {
            // TODO
            // int disp;
            // if ((d & 0x80) == 0)
            //      disp = 0x000000FF & d;
            // else
            //      disp = (int)(0xFFFFFF00 | d);
            // nextIP = (uint)(s.PC + (disp << 1) + 4);

            var temp = s.PC;
            if (s.T)
            {
                nextIP = (uint)(d.CalcAddr(s));
                delaySlot = (temp + 2);
            }
            else
            {
                nextIP = s.PC + 2;
            }
        }

        public static void Jump(this MachineState s, AddressOperand m, ref uint nextIP,
            ref uint? delaySlot)
        {
            var temp = s.PC;
            var raw = m.CalcAddr(s);
            nextIP = raw + 4;
            delaySlot = (temp + 2);
        }

        public static void JumpSubroutine(this MachineState s, Mu32Operand mem, ref uint nextIP,
            ref uint? delaySlot)
        {
            s.PR = s.PC;
            nextIP = mem.OffA(s);
            delaySlot = (s.PR + 2);
        }

        public static void ReturnFromEx(this MachineState s, ref uint nextIP,
            ref uint? delaySlot)
        {
            var temp = s.PC;
            nextIP = s.SPC;
            s.SR = (Flagged)s.SSR;
            delaySlot = (temp + 2);
        }

        public static void ReturnSubroutine(this MachineState s, ref uint nextIP,
            ref uint? delaySlot)
        {
            var temp = s.PC;
            nextIP = s.PR + 4;
            delaySlot = (temp + 2);
        }
    }
}