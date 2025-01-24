using System;
using System.Collections.Generic;
using PoViEmu.Base;
using PoViEmu.Base.ISA;
using PoViEmu.SH3.ISA;

namespace PoViEmu.Tests.ISA.Util
{
    internal sealed class NonState : IMachineState,
        IFlatMemAccess<byte>, IFlatMemAccess<ushort>, IFlatMemAccess<uint>
    {
        private readonly Dictionary<ShRegister, uint> _regs = new();

        public FlatMemAccess<byte> U8 { get; }
        public FlatMemAccess<ushort> U16 { get; }
        public FlatMemAccess<uint> U32 { get; }
        public byte[] Bytes { get; }

        public NonState()
        {
            U8 = new FlatMemAccess<byte>(this);
            U16 = new FlatMemAccess<ushort>(this);
            U32 = new FlatMemAccess<uint>(this);
            Bytes = new byte[0x50000];
        }

        public uint this[ShRegister reg]
        {
            get => _regs[reg];
            set => _regs[reg] = value;
        }

        public void Set(uint off, byte value)
        {
            throw new NotImplementedException();
        }

        public void Set(uint off, ushort value)
        {
            throw new NotImplementedException();
        }

        public void Set(uint off, uint value)
        {
            throw new NotImplementedException();
        }

        byte IFlatMemAccess<byte>.Get(uint off) => Bytes[off];
        ushort IFlatMemAccess<ushort>.Get(uint off) => Endian.ReadUInt16(Bytes, (int)off);
        uint IFlatMemAccess<uint>.Get(uint off) => Endian.ReadUInt32(Bytes, (int)off);
    }
}