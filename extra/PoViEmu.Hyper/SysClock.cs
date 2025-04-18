using System;
using System.Threading;
using System.Threading.Tasks;

namespace PoViEmu.Hyper
{
    public sealed class SysClock : IClock
    {
        private readonly Timer _timer;

        public SysClock()
        {
            _timer = new Timer(DoTick);
        }

        public void Dispose()
        {
            _timer.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await _timer.DisposeAsync();
        }

        public event EventHandler<TickEventArgs>? OnTick;

        public double TickMs { get; set; }
        public long RealMs { get; private set; }

        public double TickHz
        {
            get => 1_000.0 / TickMs;
            set => TickMs = 1_000.0 / value;
        }

        public double RealHz => 1_000.0 / RealMs;

        public int Cycles { get; set; }
        public double CyclesPerTick => Cycles / RealHz;

        public void Start()
        {
            var ms = RealMs = (long)Math.Round(TickMs);
            if (ms == 0) return;
            _timer.Change(ms, ms);
        }

        public void Stop()
        {
            const int no = Timeout.Infinite;
            _timer.Change(no, no);
        }

        private void DoTick(object? state)
        {
            OnTick?.Invoke(this, new TickEventArgs(CyclesPerTick));
        }
    }
}