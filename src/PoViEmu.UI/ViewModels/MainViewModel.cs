using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.X86Decoding;

namespace PoViEmu.UI.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        [ObservableProperty] private string _greeting = "Welcome to Avalonia!";

        public MainViewModel()
        {
            var mem = new MemoryStream([90, 04, 02, 03, 02, 51, 52, 90, 60, 21]);
            var txt = mem.Disassemble(err: false).ToText();
            Greeting = txt;
        }
    }
}