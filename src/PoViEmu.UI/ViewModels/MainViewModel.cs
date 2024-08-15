using System;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.Common;
using PoViEmu.X86Decoding;

namespace PoViEmu.UI.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        [ObservableProperty] private string _greeting = "Welcome to Avalonia!";

        public MainViewModel()
        {
            var stuffDir = SysInfo.GetEntryDir().GetChild("Stuff");
            var file = stuffDir.GetChild("sample.bin");

            // Console.WriteLine(Convert.ToHexString(File.ReadAllBytes(file)));

            using var stream = File.OpenRead(file);
            var txt = stream.Disassemble(skip: 1524, err: false).ToText();
            Greeting = txt;
        }
    }
}