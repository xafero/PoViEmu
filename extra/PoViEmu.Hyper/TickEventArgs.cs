using System;

namespace PoViEmu.Hyper
{
    public sealed class TickEventArgs : EventArgs
    {
        public TickEventArgs(double cyclesPerTick)
        {
            Cycles = cyclesPerTick;
        }

        public double Cycles { get; }
    }
}