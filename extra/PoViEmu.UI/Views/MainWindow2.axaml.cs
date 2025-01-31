using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PoViEmu.Base;

namespace FunDesk.Views
{
    public partial class MainWindow : Window
    {
        public Dictionary<string, byte[]> Files = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Control_OnLoaded(object? sender, RoutedEventArgs e)
        {
            var root = DirHelper.GetCurrentDirectory();
            var folder = DirHelper.GetFullPath(root, "..", "..",
                "test", "PoViEmu.Tests.CPU", "Resources", "Chimes");

            var sh3Com = Path.Combine(folder, "op_sh3.com");
            Files["sh3"] = File.ReadAllBytes(sh3Com);

            var x86Com = Path.Combine(folder, "op_x86.com");
            Files["x86"] = File.ReadAllBytes(x86Com);
        }
    }
}