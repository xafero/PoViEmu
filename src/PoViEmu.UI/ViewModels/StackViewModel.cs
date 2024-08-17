using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.Common;
using PoViEmu.UI.Models;
using PoViEmu.X86Decoding;

namespace PoViEmu.UI.ViewModels
{
    public partial class StackViewModel : ViewModelBase
    {
        [ObservableProperty] private ObservableCollection<StackLine> _stack = new();

        public void Init()
        {
            Stack.Clear();

            Stack.Add(new StackLine("SS", 0x0000, 0x002B));
            Stack.Add(new StackLine("SS", 0x0002, 0xC400));
            Stack.Add(new StackLine("SS", 0x0004, 0x16B4));
            Stack.Add(new StackLine("SS", 0x0006, 0xC400));
            Stack.Add(new StackLine("SS", 0x0008, 0x0BAF));
        }
    }
}