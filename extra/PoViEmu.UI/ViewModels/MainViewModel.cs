using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.Base;
using PoViEmu.Inventory.Config;
using PoViEmu.UI.Core;
using PoViEmu.UI.Routes;

namespace PoViEmu.UI.ViewModels
{
    public partial class MainViewModel : ViewModelBase, IRouter
    {
        private static readonly RingStack<IRoutable> Routed = new(10);

        [ObservableProperty] private IViewModelBase _currentView;
        [ObservableProperty] private bool _canGoBack;

        public MainViewModel()
        {
            Task.Run(StartIt);
        }

        private async Task StartIt()
        {
            var cfg = CfgRepo.Instance;
            await cfg.Load();

            var ent = cfg.Entities;
            if (ent == null)
                throw new InvalidOperationException("No handle found!");

            if (ent.Count <= 0)
            {
                this.Push<WelcomeViewModel>();
                return;
            }

            this.Push<InstanceViewModel>();
        }

        private void GoTo(IRoutable? model)
        {
            if (model is IViewModelBase vmb)
            {
                Routed.Push(model);
                CurrentView = vmb;
            }
            CanGoBack = IsGoBack();
        }

        public void Push<T>(T model) where T : IRoutable
        {
            GoTo(model);
        }

        public void GoBack()
        {
            _ = Routed.Pop();
            var now = Routed.Pop();
            if (now is INavigable nav)
                nav.OnBack();
            GoTo(now);
        }

        private static bool IsGoBack() => Routed.Count >= 2;
    }
}