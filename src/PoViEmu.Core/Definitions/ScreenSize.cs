namespace PoViEmu.Core.Definitions
{
    public record ScreenSize(int Width, int Height)
    {
        public override string ToString() => $"{Width}\u00d7{Height}";
    }
}