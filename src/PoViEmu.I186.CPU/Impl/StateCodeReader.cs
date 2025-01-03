using System;
using System.Collections.Generic;
using Iced.Intel;
using PoViEmu.Base.CPU;
using PoViEmu.I186.ISA.Decoding;

// ReSharper disable PossibleMultipleEnumeration
#pragma warning disable CS8619

namespace PoViEmu.I186.CPU.Impl
{
    internal sealed class StateCodeReader : CodeReader, ICodeReader<XInstruction>
    {
        private const int MaxLength = 15;
        private readonly MachineState _parent;
        private readonly List<byte> _bytes = [];

        public StateCodeReader(MachineState parent)
        {
            _parent = parent;
        }

        private static IEnumerable<byte> ReadBlock(MachineState m, ushort? oIp)
        {
            var cs = m.CS;
            var ip = oIp ?? m.IP;
            return m.ReadMemory(cs, ip, MaxLength);
        }

        private Decoder? _decoder;
        private IEnumerator<byte>? _enumerator;
        private Instruction _instruction;
        private ushort? _ipPtr;

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

        public override int ReadByte()
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
            var actualTgt = _parent.IP;
            var expectTgt = _instruction.IP16 + _instruction.Length;
            if (actualTgt == expectTgt)
                return;
            ResetIter(_enumerator);
            if (_decoder != null)
                _decoder.IP = actualTgt;
            _ipPtr = null;
        }

        private static Decoder CreateDecoder(CodeReader reader, ushort ip)
        {
            const DecoderOptions opt = DecoderOptions.NoInvalidCheck;
            const int bits = 16;
            return Decoder.Create(bits, reader, ip, opt);
        }

        private Decoder GetDecoder()
        {
            if (_decoder != null)
                return _decoder;

            var tool = CreateDecoder(this, _ipPtr ?? _parent.IP);
            _decoder = tool;
            return tool;
        }

        public XInstruction NextInstruction()
        {
            ResetIfJumped();
            var tool = GetDecoder();
            tool.Decode(out _instruction);
            _ipPtr = _instruction.NextIP16;
            var copy = _bytes.ToArray();
            _bytes.Clear();
            var hex = Convert.ToHexString(copy);
            return new XInstruction(hex, _instruction);
        }
    }
}