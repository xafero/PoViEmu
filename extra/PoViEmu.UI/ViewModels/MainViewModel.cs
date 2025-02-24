using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.UI.Tools;

namespace PoViEmu.UI.ViewModels
{
    public partial class MainViewModel : ViewModelBase, IRouter
    {
        private static readonly Stack<IRoutable> _routed = new(10);

        [ObservableProperty] private ViewModelBase _currentView;

        public MainViewModel()
        {
            this.Push<WelcomeViewModel>();
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