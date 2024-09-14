using System;
using System.Diagnostics;
using System.Threading;

namespace PoViEmu.Core.Hardware
{
    public class Clock : IDisposable
    {
        private readonly double _cycleCount;
        private readonly double _frameCount;
        private readonly double _factor;

        public Clock(double cycleCount = 3_000_000, double frameCount = 60, double factor = 1.017)
        {
            _cycleCount = cycleCount;
            _frameCount = frameCount;
            _factor = factor;
            Start();
        }

        private long _cycles;
        private double _accuracy;
        private double _ticksPerFrame;
        private int _cyclesPerFrame;
        private double _ticksPerMilli;
        private CancellationTokenSource _cancel;
        private Thread _thread;

        private void Start()
        {
            _cycles = 0;
            _accuracy = 1;
            var ticksPerSecond = Stopwatch.Frequency;
            _ticksPerFrame = ticksPerSecond / _frameCount;
            var ticksPerCycle = ticksPerSecond / _cycleCount;
            _cyclesPerFrame = (int)(_ticksPerFrame / ticksPerCycle * _factor);
            if (_cyclesPerFrame <= 0)
                throw new InvalidOperationException("No CPU cycles possible!");

            _ticksPerMilli = ticksPerSecond / 1000.0;
            _cancel = new CancellationTokenSource();
            _thread = new Thread(DoLoop)
            {
                Name = $"{nameof(Clock)}{nameof(Thread)}",
                Priority = ThreadPriority.Highest,
                IsBackground = true
            };
            _thread.Start();
        }

        private void DoLoop()
        {
            var watch = Stopwatch.StartNew();
            while (!_cancel.IsCancellationRequested)
            {
                var startTicks = Stopwatch.GetTimestamp();

                DoExecute();

                if (watch.ElapsedMilliseconds >= 1_000)
                {
                    _accuracy = _cycles / _cycleCount;
                    Log();
                    _cycles = 0;
                    watch.Restart();
                }

                var waitDelay = (int)Math.Ceiling((_ticksPerFrame -
                                                   (Stopwatch.GetTimestamp() - startTicks))
                                                  / _ticksPerMilli);
                if (waitDelay >= 0)
                    Thread.Sleep(waitDelay);
            }
        }

        protected virtual void Log()
        {
            Console.WriteLine($" {DateTime.Now:hh:mm:ss.fffffff} {_accuracy:P2} {_cycles}");
        }

        protected virtual void DoExecute()
        {
            _cycles += _cyclesPerFrame;
        }

        private void Stop()
        {
            _cancel?.Cancel();
            _cancel?.Dispose();
            _cancel = null;
            _thread?.Interrupt();
            _thread = null;
        }

        public void Dispose() => Stop();

        public override string ToString()
        {
            return $"Clock for {_cycleCount} cycles and {_frameCount} video frames";
        }
    }
}