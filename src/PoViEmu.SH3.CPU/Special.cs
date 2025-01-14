using PoViEmu.SH3.CPU.Soft;

namespace PoViEmu.SH3.CPU
{
    internal static class Special
    {
        public static void Sleep(this MachineState _, SH7291 cpu, ref uint nextIP)
        {
            nextIP -= 2;
            // TODO Sleep Mode
            cpu.Halted = true;
        }

        public static void Trapa(this MachineState s, SH7291 cpu, sbyte i)
        {
            /*
            // TODO
            //
            long immT;
            immT=(0x000000FF & i);
            TRA=immT<<2;
            s.SSR = (uint)s.SR;
            s.SPC = s.PC;
            s.MD = true;
            s.BL = true;
            s.RB = true;
            s.EXPEVT=0x00000160;
            s.PC=s.VBR+0x00000100;
            //
            */

            cpu.ExecuteInterrupt((byte)i, s);
            if ((cpu.InterruptTable[0x21] as DOSInterrupts)?.ReturnCode is not null)
                cpu.Halted = true;
        }
    }
}