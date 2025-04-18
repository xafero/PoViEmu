using System.Threading.Tasks;
using PoViEmu.Base.CPU;

namespace PoViEmu.Hyper
{
    public abstract class BaseMachine<TC, TM> : IVMachine where TC : ICpu where TM : IState
    {
        protected readonly TC _cpu;
        protected readonly TM _state;
        private readonly IClock _clock;

        protected BaseMachine(TC cpu, TM state)
        {
            _cpu = cpu;
            _state = state;
            _clock = CreateClock();
        }

        private static SysClock CreateClock()
        {
            var clock = new SysClock();
            clock.Cycles = 2;
            clock.TickHz = 1;
            clock.OnTick += ClockOnTick;
            return clock;
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