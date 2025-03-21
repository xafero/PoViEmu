using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using PoViEmu.UI.Core;
using PoViEmu.UI.Extensions;
using PoViEmu.UI.ViewModels;
using PoViEmu.UI.Views;

namespace PoViEmu.UI
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            // TODO DisableAvaloniaDataAnnotationValidation();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainViewModel()
                };
                ExtPoints.Instance.OnDesktopInit(this, desktop.MainWindow);
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime mobile)
            {
                mobile.MainView = new MainView
                {
                    DataContext = new MainViewModel()
                };
                ExtPoints.Instance.OnMobileInit(this, mobile.MainView);
            }

            base.OnFrameworkInitializationCompleted();
        }

        private static void DisableAvaloniaDataAnnotationValidation()
        {
            var dataValidationPluginsToRemove =
                BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

            foreach (var plugin in dataValidationPluginsToRemove)
            {
                BindingPlugins.DataValidators.Remove(plugin);
            }
        }
    }
}