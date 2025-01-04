using System;
using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PoViEmu.UI.ViewModels
{
    public partial class SimCtrlViewModel : ViewModelBase
    {
        [ObservableProperty] private TimeSpan _cpuTime;
        [ObservableProperty] private bool _shouldRun;

        private Thread _thread;

        public void Init()
        {
            CpuTime = TimeSpan.Zero;
            ShouldRun = true;

            _thread = new Thread(OnLoop) { IsBackground = true };
            _thread.Start();
        }

        private void OnLoop()
        {
            while (ShouldRun)
            {
                CpuTime = CpuTime.Add(TimeSpan.FromSeconds(1));
                Thread.Sleep(1 * 500);
            }
        }
    }
}