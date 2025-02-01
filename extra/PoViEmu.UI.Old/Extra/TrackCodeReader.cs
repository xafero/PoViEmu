using Iced.Intel;

namespace PoViEmu.UI.Extra
{
    public abstract class TrackCodeReader : CodeReader
    {
        public abstract (int off, byte[] bit) GetReadBytes();
    }
}