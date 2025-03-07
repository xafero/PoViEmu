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

        [ObservableProperty] private ViewModelBase _currentView;
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
            if (model is ViewModelBase vmb)
                CurrentView = vmb;
            CanGoBack = IsGoBack();
        }

        public void Push<T>(T model) where T : IRoutable
        {
            Routed.Push(model);
            GoTo(model);
        }

        public void GoBack()
        {
            _ = Routed.Pop();
            GoTo(Routed.Pop());
        }

        private bool IsGoBack() => Routed.Count >= 2;
    }
}