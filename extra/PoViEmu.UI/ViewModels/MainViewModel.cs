using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.Inventory.Config;
using PoViEmu.UI.Tools;

namespace PoViEmu.UI.ViewModels
{
    public partial class MainViewModel : ViewModelBase, IRouter
    {
        private static readonly Stack<IRoutable> _routed = new(10);

        [ObservableProperty] private ViewModelBase _currentView;

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

            throw new NotImplementedException("What to do here?!"); // TODO
        }

        public void Push<T>(T model) where T : IRoutable
        {
            _routed.Push(model);
            var vmb = (model as ViewModelBase)!;
            CurrentView = vmb;
        }

        public static IRoutable Last => _routed.Pop();
    }
}