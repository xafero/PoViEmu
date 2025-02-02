using System;
using System.Collections.Generic;
using PoViEmu.Base.CPU;
using PoViEmu.SH3.ISA.Decoding;

// ReSharper disable InconsistentNaming
// ReSharper disable PossibleMultipleEnumeration

#pragma warning disable CS8619

namespace PoViEmu.SH3.CPU.Impl
{
    internal sealed class StateCodeReader : IByteReader, ICodeReader<XInstruction>
    {
        private const int MaxLength = 7;
        private readonly ICodeState _parent;
        private readonly List<byte> _bytes = [];

        public StateCodeReader(ICodeState parent)
        {
            _parent = parent;
        }

        private static uint GetPC(ICodeState m)
        {
            if (m.dPC is { } dPc)
                return dPc;
            return m.PC;
        }

        private static IEnumerable<byte> ReadBlock(ICodeState m, uint? oIp)
        {
            var ip = oIp ?? GetPC(m);
            return m.ReadMemory(ip, MaxLength);
        }

        private Decoder? _decoder;
        private IEnumerator<byte>? _enumerator;
        private Instruction? _instruction;
        private uint? _ipPtr;

        private void ResetIter(IEnumerator<byte>? iter)
        {
            using (iter)
                _enumerator = null;
        }

        private IEnumerator<byte> GetEnumerator()
        {
            if (_enumerator != null)
                return _enumerator;

            var block = ReadBlock(_parent, _ipPtr);
            _enumerator = block.GetEnumerator();
            return _enumerator;
        }

        public byte ReadByte()
        {
            var iter = GetEnumerator();
            if (!iter.MoveNext())
            {
                ResetIter(iter);
                _ipPtr += (ushort)_bytes.Count;
                return ReadByte();
            }

            var value = iter.Current;
            _bytes.Add(value);
            return value;
        }

        private void ResetIfJumped()
        {
            if (_ipPtr == null)
                return;
            var actualTgt = GetPC(_parent);
            var expectTgt = _instruction?.IP32 + _instruction?.Length;
            if (actualTgt == expectTgt)
                return;
            ResetIter(_enumerator);
            if (_decoder != null)
                _decoder.PC = actualTgt;
            _ipPtr = null;
        }

        private static Decoder CreateDecoder(IByteReader reader, uint ip)
        {
            const DecoderOptions opt = DecoderOptions.NoInvalidCheck;
            const int bits = 16;
            return Decoder.Create(bits, reader, ip, opt);
        }

        private Decoder GetDecoder()
        {
            if (_decoder != null)
                return _decoder;

            var tool = CreateDecoder(this, _ipPtr ?? GetPC(_parent));
            _decoder = tool;
            return tool;
        }

        public XInstruction NextInstruction()
        {
            ResetIfJumped();
            var tool = GetDecoder();
            tool.Decode(out _instruction);
            _ipPtr = _instruction.NextIP32;
            var copy = _bytes.ToArray();
            _bytes.Clear();
            var hex = Convert.ToHexString(copy);
            return new XInstruction(hex, _instruction);
        }
    }
}