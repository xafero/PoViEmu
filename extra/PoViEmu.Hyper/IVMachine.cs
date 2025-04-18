using System;

namespace PoViEmu.Hyper
{
    public interface IVMachine : IDisposable, IAsyncDisposable
    {
        void Start();

        void Stop();
    }
}