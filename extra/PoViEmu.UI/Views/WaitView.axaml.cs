using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using PoViEmu.UI.Tools;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Views
{
    public partial class WaitView : UserControl
    {
        public WaitView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
            var ctx = this.GetOrCreateData<WaitViewModel>();
            ctx.Interval = 500;
            ctx.FrameMin = 1;
            ctx.FrameMax = 6;
            ctx.FrameNo = 1;
            ctx.Message = "Downloading";
            ctx.SubText = "http://www.google.ca";
            ctx.PerOne = 46;
            ctx.PerAll = 83;
            SetUrl();
            var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(ctx.Interval) };
            timer.Tick += DoNextFrame;
            timer.Start();
        }

        private void DoNextFrame(object? sender, EventArgs e)
        {
            var ctx = this.GetOrCreateData<WaitViewModel>();
            ctx.FrameNo++;
            if (ctx.FrameNo > ctx.FrameMax)
                ctx.FrameNo = ctx.FrameMin;
            SetUrl();
        }

        private void SetUrl()
        {
            var ctx = this.GetOrCreateData<WaitViewModel>();
            var url = new Uri($"avares://PoViEmu.UI/Assets/Anim/Earth/{ctx.FrameNo}.png");
            ctx.Image = new Bitmap(AssetLoader.Open(url));
        }
    }
}