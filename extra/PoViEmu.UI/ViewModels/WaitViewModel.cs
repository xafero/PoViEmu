using System;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PoViEmu.UI.ViewModels
{
    public partial class WaitViewModel : ViewModelBase
    {
        [ObservableProperty] private IImage? _image;

        [ObservableProperty] private int _frameMin;

        [ObservableProperty] private int _frameMax;

        [ObservableProperty] private int _frameNo;

        [ObservableProperty] private int _interval;

        [ObservableProperty] private string _message;

        [ObservableProperty] private string _subText;

        [ObservableProperty] private int _perOne;

        [ObservableProperty] private int _perAll;
    }
}