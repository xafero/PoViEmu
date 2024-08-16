namespace PoViEmu.Core.Meta
{
    public abstract class Video
    {
        protected Video(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; }
        public int Height { get; }
    }
}