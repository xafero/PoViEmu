using System;
using PoViEmu.Base.CPU;

namespace PoViEmu.Hyper
{
    public interface IVMachine : IDisposable, IAsyncDisposable
    {
        ICpu? Cpu { get; }

        IState? State { get; }

        IClock Clock { get; }

        void Start();

        void Stop();
    }
}