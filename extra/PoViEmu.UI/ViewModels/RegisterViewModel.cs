using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.I186.CPU;
using PoViEmu.I186.ISA;
using PoViEmu.UI.Models;
using F = PoViEmu.I186.ISA.Flagged;
using R = PoViEmu.I186.ISA.B16Register;

namespace PoViEmu.UI.ViewModels
{
    public partial class RegisterViewModel : ViewModelBase
    {
        [ObservableProperty] private MachineState _m = new();

        [ObservableProperty] private ObservableCollection<ShortLine> _regLines = [];
        [ObservableProperty] private ObservableCollection<BoolLine> _flaLines = [];

        public void Init()
        {
            M[R.AX] = 0x0001;
            M[R.BX] = 0x0002;
            M[R.CX] = 0x0003;
            M[R.DX] = 0x0004;
            M[R.SI] = 0x0005;
            M[R.DI] = 0x0006;
            M[R.DS] = 0x0007;
            M[R.ES] = 0x0008;
            M[R.SS] = 0x0009;
            M[R.SP] = 0x000A;
            M[R.BP] = 0x000B;
            M[R.CS] = 0xFFFF;
            M[R.IP] = 0x000C;

            M.CF = true;
            M.ZF = false;
            M.SF = true;
            M.DF = false;
            M.IF = true;
            M.OF = false;
            M.PF = false;
            M.AF = true;

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
            => []; // TODO FlaReg.Registers.Select(x => new ShortLine($"{x.Key}", x.Value)).ToArray();

        private BoolLine[] FlagLines
            => []; // FlaReg.Flags.Select(x => new BoolLine($"{x.Key}", x.Value)).ToArray();
    }
}