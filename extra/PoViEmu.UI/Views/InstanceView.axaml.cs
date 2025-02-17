using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using PoViEmu.UI.Tools;
using PoViEmu.UI.ViewModels;

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
            ctx.TemplateName = "Sample template!";
            ctx.InstanceName = "Sample instance?";
            ctx.InstanceNotes = "I don't really know\n What to write here?";
        }

        private void OnNextClick(object? sender, RoutedEventArgs e)
        {
            if (this.FindData<MainViewModel>() is { } mv)
            {
                mv.CurrentView = null;
            }
        }
    }
}