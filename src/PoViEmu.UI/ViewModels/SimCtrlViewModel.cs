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
    public partial class SimCtrlViewModel : ViewModelBase
    {
        [ObservableProperty] private string _title = string.Empty;

        public void Init()
        {
        }
    }
}