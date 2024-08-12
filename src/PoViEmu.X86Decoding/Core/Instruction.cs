using System;
using System.Collections.Generic;
using System.Linq;
using PoViEmu.Common;
using PoViEmu.X86Decoding.Args;
using PoViEmu.X86Decoding.Ops;

namespace PoViEmu.X86Decoding.Core
{
    public readonly record struct Instruction(
        long Offset,
        byte Code,
        int Size,
        OpCode Op,
        OpArg?[]? Args,
        byte?[]? Extra = null,
        Modifier? Mod = null
    )
    {
        public byte[] Bytes => FetchBytes(this);

        public override string ToString()
        {
            var hex = Convert.ToHexString(Bytes);
            var space = TextHelper.Space(18 - hex.Length);
            var op = Op.ToString();
            if (Mod != null) op += $" {Mod}";
            var args = Args ?? [];
            var reg = string.Join(",", args.Select(a => a?.ToString()));
            var txt = $"{Offset:X8}  {hex}{space}{op} {reg}";
            return txt.Trim();
        }

        private static byte[] FetchBytes(Instruction i)
        {
            var args = i.Args ?? [];
            foreach (var arg in args.OfType<ICalcArg>()) arg.Parent = i;
            IEnumerable<byte> bytes = new[] { i.Code };
            if (i.Extra is { } extra) bytes = bytes.Concat(extra.OfType<byte>());
            return bytes.Concat(args.OfType<IByteArg>()
                .SelectMany(a => a.Bytes)).ToArray();
        }
    }
}