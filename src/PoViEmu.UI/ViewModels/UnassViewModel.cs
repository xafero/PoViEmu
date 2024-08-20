﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.Common;
using PoViEmu.UI.Models;
// using PoViEmu.X86Decoding;

namespace PoViEmu.UI.ViewModels
{
    public partial class UnassViewModel : ViewModelBase
    {
        [ObservableProperty] private ObservableCollection<AssemblyLine> _assembly = new();

        public void Init()
        {
            var stuffDir = SysInfo.GetEntryDir<UnassViewModel>().GetChild("Stuff");
            var file = stuffDir.GetChild("sample.bin");

            using var stream = File.OpenRead(file);
            var lines = new string[0]; //stream.Disassemble(skip: 1524, err: false).ToLines();
            var objects = lines.Select(l =>
            {
                var parts = TextHelper.SplitOn(l);
                return new AssemblyLine(parts[0], parts[1], parts[2]);
            });
            Assembly.Clear();
            Array.ForEach(objects.Skip(4).Take(60).ToArray(), o => Assembly.Add(o));
        }
    }
}