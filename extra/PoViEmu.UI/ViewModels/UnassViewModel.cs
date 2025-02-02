using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.Base;
using PoViEmu.Base.CPU;
using PoViEmu.Base.ISA;
using PoViEmu.UI.Models;
using System.Collections.Generic;
using System.Linq;

namespace PoViEmu.UI.ViewModels
{
    public partial class UnassViewModel : ViewModelBase
    {
        [ObservableProperty] private ObservableCollection<BytesLine> _lines = new();

        public void Read<T>(ICodeReader<T> reader, int maxCount = 60) where T : IInstruction
        {
            var lines = new List<string>();
            while (lines.Count < maxCount)
            {
                var instr = reader.NextInstruction();
                lines.Add(instr.ToString() ?? "!");
            }
            var objects = lines.Select(l =>
            {
                var parts = TextHelper.SplitOn(l, 3);
                return new BytesLine(parts[0], parts[1], parts[2]);
            });
            Lines.Clear();
            Array.ForEach(objects.ToArray(), o => Lines.Add(o));
        }
    }
}