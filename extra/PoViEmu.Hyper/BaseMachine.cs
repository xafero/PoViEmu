using System.Threading.Tasks;
using PoViEmu.Base.CPU;

// ReSharper disable VirtualMemberNeverOverridden.Global

namespace PoViEmu.Hyper
{
    public abstract class BaseMachine<TC, TM> : IVMachine where TC : ICpu where TM : IState
    {
        protected readonly TC _cpu;
        protected readonly TM _state;
        protected readonly IClock _clock;

        protected BaseMachine(TC cpu, TM state)
        {
            _cpu = cpu;
            _state = state;

            var clock = new SysClock();
            clock.OnTick += ClockOnTick;
            _clock = clock;
        }

        public void Start()
        {
            _clock.Start();
        }

        public void Stop()
        {
            _clock.Stop();
        }

        private static void ClockOnTick(object? sender, TickEventArgs e)
        {









            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            _clock.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await _clock.DisposeAsync();
        }
    }
}