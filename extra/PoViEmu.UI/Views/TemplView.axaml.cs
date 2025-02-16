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

        private void Control_OnLoaded(object? sender, RoutedEventArgs e)
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
                    ctx.Debug = string.Join(", ", entries.Select(y => y.Name));
                },
                _ =>
                {
                    ctx.Debug = "Could not contact the server!";
                });
        }
    }
}