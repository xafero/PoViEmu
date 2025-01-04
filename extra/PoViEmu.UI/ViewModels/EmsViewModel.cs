using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.I186.CPU;
using PoViEmu.UI.Models;

namespace PoViEmu.UI.ViewModels
{
    public partial class EmsViewModel : ViewModelBase
    {
        [ObservableProperty] private MachineState _m = new();

        [ObservableProperty] private ObservableCollection<ShortLine> _bnkLines = [];
        [ObservableProperty] private ObservableCollection<ShortLine> _fraLines = [];

        public void Init()
        {
            M.Bk0 = 0x0001;
            M.Bk1 = 0x0002;
            M.Bk2 = 0x0003;
            M.Bk3 = 0x0004;
            M.Bk4 = 0x0005;
            M.Bk5 = 0x0006;
            M.Bk6 = 0x0007;

            M.Fr0 = 0x0010;
            M.Fr1 = 0x0020;
            M.Fr2 = 0x0030;
            M.Fr3 = 0x0040;
            M.Fr4 = 0x0050;
            M.Fr5 = 0x0060;
            M.Fr6 = 0x0070;
            M.Fr7 = 0x0011;
            M.Fr8 = 0x0021;
            M.Fr9 = 0x0031;
            M.Fr10 = 0x0041;
            M.Fr11 = 0x0051;

            BnkLines.Clear();
            FraLines.Clear();
            Update();
        }

        private void Update()
        {
            Array.ForEach(BankLines, l => BnkLines.Add(l));
            Array.ForEach(FrameLines, l => FraLines.Add(l));
        }

        private ShortLine[] BankLines
            => []; // TODO Ems.Banks.Select(x => new ShortLine($"{x.Key}", x.Value)).ToArray();

        private ShortLine[] FrameLines
            => []; // TODO Ems.Frames.Select(x => new ShortLine($"{x.Key}", x.Value)).ToArray();
    }
}