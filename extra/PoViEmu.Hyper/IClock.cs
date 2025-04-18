using System;

namespace PoViEmu.Hyper
{
    public interface IClock : IDisposable, IAsyncDisposable
    {
        void Start();

        void Stop();

        event EventHandler<TickEventArgs> OnTick;
    }
}