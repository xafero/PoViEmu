using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using PoViEmu.Inventory.Upper;
using PoViEmu.UI.Tools;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Views
{
    public partial class TemplView : UserControl
    {
        public TemplView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
            var ctx = this.GetOrCreateData<TemplViewModel>();
            CtxUtil.Invoke(async () =>
                {
                    var repo = TemplRepo.Instance;
                    await repo.Load();
                    return repo.AllTemplates.ToArray();
                },
                entries =>
                {
                    ctx.Templates = entries;
                },
                _ =>
                {
                    ctx.Debug = "Could not contact the server!";
                });
        }

        private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            var selected = e.AddedItems.Cast<TemplEntry>().FirstOrDefault();
            if (selected == null)
                return;
            var ctx = this.GetOrCreateData<TemplViewModel>();
            ctx.Selected = selected;
            ctx.ShowNextBtn = true;
        }

        private void NextBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            var ctx = this.GetOrCreateData<TemplViewModel>();
            ctx.Debug = $" ! {ctx.Selected.Name}";
            
            return;
            if (this.FindData<MainViewModel>() is { } mv)
            {
                mv.CurrentView = null;
            }
        }
    }
}