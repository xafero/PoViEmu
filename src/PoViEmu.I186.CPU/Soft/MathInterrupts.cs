using System;

namespace PoViEmu.I186.CPU.Soft
{
    public sealed class MathInterrupts : IInterruptHandler
    {
        public const byte OverflowNo = 0x04;

        public void Handle(byte num, MachineState m)
        {
            switch (num)
            {
                case OverflowNo:
                    // TODO: Handle this?!
                    return;
            }
            throw new InvalidOperationException($"Missing math interrupt 0x{num:X2}!");
        }
    }
}