using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using PoViEmu.UI.Core;

namespace PoViEmu.UI.Tools
{
    public class ViewLocator : IDataTemplate
    {
        public Control? Build(object? data)
        {
            if (data is null)
                return null;

            var sdt = data.GetType();
            var name = sdt.FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
            var type = sdt.Assembly.GetType(name);

            if (type != null)
            {
                return (Control)Activator.CreateInstance(type)!;
            }

            return new TextBlock { Text = $"Not Found: {name}" };
        }

        public bool Match(object? data)
        {
            return data is IViewModelBase;
        }
    }
}