using System;
using System.Collections.Generic;
using Iced.Intel;
using PoViEmu.Core.Decoding;
using PoViEmu.Core.Hardware;

// ReSharper disable PossibleMultipleEnumeration

namespace PoViEmu.Core.Compat
{
    public sealed class StateCodeReader : CodeReader
    {
        private const int MaxLength = 15;
        private readonly MachineState _parent;
        private readonly List<byte> _bytes = [];

        public StateCodeReader(MachineState parent)
        {
            _parent = parent;
        }

        private static Decoder CreateDecoder(CodeReader reader, ushort ip)
        {
            const DecoderOptions opt = DecoderOptions.NoInvalidCheck;
            const int bits = 16;
            return Decoder.Create(bits, reader, ip, opt);
        }

        private static IEnumerable<byte> ReadBlock(MachineState m)
        {
            var cs = m.CS;
            var ip = m.IP;
            return m.ReadMemory(cs, ip, MaxLength);
        }

        private Decoder? _decoder;
        private IEnumerator<byte>? _enumerator;
        private Instruction _instruction;

        private Decoder GetDecoder()
        {
            if (_decoder != null)
                return _decoder;

            var tool = CreateDecoder(this, _parent.IP);
            _decoder = tool;
            return tool;
        }

        private IEnumerator<byte> GetEnumerator()
        {
            if (_enumerator != null)
                return _enumerator;

            var block = ReadBlock(_parent);
            _enumerator = block.GetEnumerator();
            return _enumerator;
        }

        public override int ReadByte()
        {
            var iter = GetEnumerator();
            if (!iter.MoveNext())
                using (iter)
                {
                    _enumerator = null;
                    _parent.IP += (ushort)_bytes.Count;
                    return ReadByte();
                }

            var value = iter.Current;
            _bytes.Add(value);
            return value;
        }

        public XInstruction NextInstruction()
        {
            var tool = GetDecoder();
            tool.Decode(out _instruction);
            _parent.IP = _instruction.NextIP16;
            var copy = _bytes.ToArray();
            _bytes.Clear();
            var hex = Convert.ToHexString(copy);
            return new XInstruction(hex, _instruction);
        }
    }
}