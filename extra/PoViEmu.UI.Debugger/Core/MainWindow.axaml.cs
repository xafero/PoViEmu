using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace PoViEmu.UI.Dbg.Core
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Control_OnLoaded(object? sender, RoutedEventArgs e)
        {
        }
        
        private void Control_OnClosed(object? sender, EventArgs e)
        {
            (Owner as Window)?.Close();
        }
    }
}