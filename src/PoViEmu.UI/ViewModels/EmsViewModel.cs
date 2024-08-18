using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.Common;
using PoViEmu.Core.Meta;
using PoViEmu.UI.Models;
using PoViEmu.X86Decoding;
using B = PoViEmu.Core.Meta.EmsBank;
using F = PoViEmu.Core.Meta.EmsFrame;

namespace PoViEmu.UI.ViewModels
{
    public partial class EmsViewModel : ViewModelBase
    {
        [ObservableProperty] private Ems _ems = new();

        [ObservableProperty] private ObservableCollection<ShortLine> _bnkLines = new();
        [ObservableProperty] private ObservableCollection<ShortLine> _fraLines = new();

        public void Init()
        {
            Ems.Clear();

            var r = Ems.Banks;
            r[B.Bank0] = 0x0001;
            r[B.Bank1] = 0x0002;
            r[B.Bank2] = 0x0003;
            r[B.Bank3] = 0x0004;
            r[B.Bank4] = 0x0005;
            r[B.Bank5] = 0x0006;
            r[B.Bank6] = 0x0007;

            var f = Ems.Frames;
            f[F.Frame0] = 0x0010;
            f[F.Frame1] = 0x0020;
            f[F.Frame2] = 0x0030;
            f[F.Frame3] = 0x0040;
            f[F.Frame4] = 0x0050;
            f[F.Frame5] = 0x0060;
            f[F.Frame6] = 0x0070;
            f[F.Frame7] = 0x0011;
            f[F.Frame8] = 0x0021;
            f[F.Frame9] = 0x0031;
            f[F.Frame10] = 0x0041;
            f[F.Frame11] = 0x0051;

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
            => Ems.Banks.Select(x => new ShortLine($"{x.Key}", x.Value)).ToArray();

        private ShortLine[] FrameLines
            => Ems.Frames.Select(x => new ShortLine($"{x.Key}", x.Value)).ToArray();
    }
}