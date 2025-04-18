using System;

namespace PoViEmu.Hyper
{
    public interface IClock : IDisposable, IAsyncDisposable
    {
        void Start();

        void Stop();

        int Cycles { set; }

        double TickHz { set; }
    }
}