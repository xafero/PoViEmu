using System.Linq;
using PoViEmu.Core.Machine.Decoding;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Args
{
    public sealed class MemArg : OpArg, IByteArg
    {
        public MemArg(byte a, byte b)
        {
            A = a;
            B = b;
        }

        public MemArg(byte a, short b)
        {
            A = a;
            B = b;
        }

        public MemArg(short a, byte b)
        {
            A = a;
            B = b;
        }

        public MemArg(short a, short b)
        {
            A = a;
            B = b;
        }

        public object A { get; }
        public object B { get; }

        public override string ToString()
        {
            return $"0x{B:x}:0x{A:x}";
        }

        public byte[] Bytes => A.ToLittleEndian().Concat(B.ToLittleEndian()).ToArray();
    }
}