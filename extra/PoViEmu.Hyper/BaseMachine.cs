using System.Threading.Tasks;
using PoViEmu.Base.CPU;

// ReSharper disable VirtualMemberNeverOverridden.Global

namespace PoViEmu.Hyper
{
    public abstract class BaseMachine<TC, TM> : IVMachine
        where TC : ICpu
        where TM : IState
    {
        protected TC? Cpu { get; set; }
        protected TM? State { get; set; }
        protected IClock Clock { get; }

        protected BaseMachine()
        {
            var clock = new SysClock();
            clock.OnTick += ClockOnTick;
            Clock = clock;
        }

        public void Start()
        {
            Clock.Start();
        }

        public void Stop()
        {
            Clock.Stop();
        }

        protected abstract void ClockOnTick(object? sender, TickEventArgs e);

        public void Dispose()
        {
            Clock.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await Clock.DisposeAsync();
        }
    }
}