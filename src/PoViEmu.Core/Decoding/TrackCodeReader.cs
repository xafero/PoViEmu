using Iced.Intel;

namespace PoViEmu.Core.Decoding
{
    public abstract class TrackCodeReader : CodeReader
    {
        public abstract (int off, byte[] bit) GetReadBytes();
    }
}