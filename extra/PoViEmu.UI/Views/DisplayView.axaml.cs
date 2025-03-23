using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using PoViEmu.Base.CPU;
using PoViEmu.UI.Graphics;

namespace PoViEmu.UI.Views
{
    public partial class DisplayView : UserControl
    {
        public new static readonly StyledProperty<int?> WidthProperty =
            AvaloniaProperty.Register<DisplayView, int?>(nameof(Width));

        public new int? Width
        {
            get => GetValue(WidthProperty);
            set => SetValue(WidthProperty, value);
        }

        public new static readonly StyledProperty<int?> HeightProperty =
            AvaloniaProperty.Register<DisplayView, int?>(nameof(Height));

        public new int? Height
        {
            get => GetValue(HeightProperty);
            set => SetValue(HeightProperty, value);
        }

        public static readonly StyledProperty<int?> FramesProperty =
            AvaloniaProperty.Register<DisplayView, int?>(nameof(Frames));

        public int? Frames
        {
            get => GetValue(FramesProperty);
            set => SetValue(FramesProperty, value);
        }

        public static readonly StyledProperty<IState?> StateProperty =
            AvaloniaProperty.Register<DisplayView, IState?>(nameof(State));

        public IState? State
        {
            get => GetValue(StateProperty);
            set => SetValue(StateProperty, value);
        }

        public DisplayView()
        {
            InitializeComponent();
        }

        private WriteableBitmap _bitmap;
        private DispatcherTimer _timer;
        private byte[] _pixelData;

        private void Control_OnLoaded(object? sender, RoutedEventArgs e)
        {
            var width = Width ?? 160;
            var height = Height ?? 160;
            var frames = Frames ?? 60;
            _bitmap = DrawTool.CreateBitmap(width, height);
            _pixelData = DrawTool.CreatePixels(width, height);
            ImgCanvas.Source = _bitmap;
            StartRenderLoop(frames);
        }

        private void StartRenderLoop(int frames)
        {
            _timer = LoopTool.CreateTimer(frames);
            _timer.Tick += Render;
            _timer.Start();
        }

        private void Render(object? _, EventArgs __)
        {
            DrawTool.UpdatePixels(_pixelData);
            _bitmap.CopyFrom(_pixelData);
            ImgCanvas.InvalidateVisual();
        }
    }
}