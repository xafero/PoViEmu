using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.Inventory.Config;
using PoViEmu.UI.Tools;
using PoViEmu.UI.ViewModels;
using PoViEmu.UI.Routes;

namespace PoViEmu.UI.Views
{
    public partial class InstanceView : UserControl
    {
        public InstanceView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
            var ctx = this.GetOrCreateData<InstanceViewModel>();
            CtxUtil.Invoke(async () =>
                {
                    var repo = CfgRepo.Instance;
                    await repo.Load();
                    return repo.Entities.Values.ToArray();
                },
                entries => { ctx.Instances = entries; },
                _ => { ctx.Debug = "Could not load instances!"; });
        }

        private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            var selected = e.AddedItems.Cast<OneEntity>().FirstOrDefault();
            if (selected == null)
                return;
            var ctx = this.GetOrCreateData<InstanceViewModel>();
            ctx.Selected = selected;
            ctx.ShowNextBtn = true;
        }

        private Guid SelectedId
        {
            get
            {
                var ctx = this.GetOrCreateData<InstanceViewModel>();
                var instId = ctx.Selected.Id;
                return instId;
            }
        }

        private void RunBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            this.GetRouter().Push(new RunInstViewModel { InstanceId = SelectedId });
        }

        private void EditBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            this.GetRouter().Push(new EditInstViewModel { InstanceId = SelectedId });
        }

        private void DelBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            this.GetRouter().Push(new DelInstViewModel { InstanceId = SelectedId });
        }

        private void NewBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            this.GetRouter().Push<TemplViewModel>();
        }
    }
}