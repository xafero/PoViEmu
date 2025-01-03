using System;
using PoViEmu.SH3.ISA.Core;

namespace PoViEmu.SH3.ISA.Decoding
{
    public sealed class Decoder
    {
        public uint PC { get; set; }

        private readonly IByteReader _reader;

        private Decoder(int _, IByteReader reader, uint ip, DecoderOptions __)
        {
            _reader = reader;
            PC = ip;
        }

        public void Decode(out Instruction instr)
        {
            const uint step = 2;
            var current = Parser.Parse(_reader);
            current.IP32 = PC;
            current.NextIP32 = PC + step;
            PC += step;
            instr = current;
        }

        public static Decoder Create(int bits, IByteReader reader, uint ip, DecoderOptions opt)
        {
            return bits switch
            {
                16 => new Decoder(bits, reader, ip, opt),
                _ => throw new InvalidOperationException($"{bits} bits are not supported!")
            };
        }
    }
}