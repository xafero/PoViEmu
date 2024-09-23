using PoViEmu.Common;

namespace PoViEmu.Core.Modules
{
    public sealed class PvaInfo
    {
        /*internal*/ public readonly PvaHeader _real;

        public PvaInfo(PvaHeader header)
            => _real = header;

        public string Signature
            => TextHelper.CleanUp(new string(_real.Signature));

        public string Name
            => TextHelper.CleanUp(new string(_real.ProgramName));
    }
}