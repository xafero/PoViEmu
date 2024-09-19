using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.Core.Meta;
using PoViEmu.UI.Models;
using F = PoViEmu.Core.Meta.CpuFlag;
using R = PoViEmu.Core.Meta.CpuRegister;

namespace PoViEmu.UI.ViewModels
{
    public partial class RegisterViewModel : ViewModelBase
    {
        [ObservableProperty] private FlaReg _flaReg = new();

        [ObservableProperty] private ObservableCollection<ShortLine> _regLines = [];
        [ObservableProperty] private ObservableCollection<BoolLine> _flaLines = [];

        public void Init()
        {
            FlaReg.Clear();

            var r = FlaReg.Registers;
            r[R.AX] = 0x0001;
            r[R.BX] = 0x0002;
            r[R.CX] = 0x0003;
            r[R.DX] = 0x0004;
            r[R.SI] = 0x0005;
            r[R.DI] = 0x0006;
            r[R.DS] = 0x0007;
            r[R.ES] = 0x0008;
            r[R.SS] = 0x0009;
            r[R.SP] = 0x000A;
            r[R.BP] = 0x000B;
            r[R.CS] = 0xFFFF;
            r[R.IP] = 0x000C;

            var f = FlaReg.Flags;
            f[F.CF] = true;
            f[F.ZF] = false;
            f[F.SF] = true;
            f[F.DF] = false;
            f[F.IF] = true;
            f[F.OF] = false;
            f[F.PF] = false;
            f[F.AF] = true;

            RegLines.Clear();
            FlaLines.Clear();
            Update();
        }

        private void Update()
        {
            Array.ForEach(RegisterLines, l => RegLines.Add(l));
            Array.ForEach(FlagLines, l => FlaLines.Add(l));
        }

        private ShortLine[] RegisterLines
            => FlaReg.Registers.Select(x => new ShortLine($"{x.Key}", x.Value)).ToArray();

        private BoolLine[] FlagLines
            => FlaReg.Flags.Select(x => new BoolLine($"{x.Key}", x.Value)).ToArray();
    }
}