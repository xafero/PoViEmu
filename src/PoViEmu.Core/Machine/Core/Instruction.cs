using System;
using System.IO;
using PoViEmu.Core.Machine.Args;
using PoViEmu.Core.Machine.Ops;
using System;
using System.Collections.Generic;
using System.Linq;
using PoViEmu.Common;
using PoViEmu.Core.Machine.Args;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Core
{
    public readonly record struct Instruction(
        long Offset,
        byte Code,
        int Size,
        OpCode Op,
        OpArg?[] Args,
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
            foreach (var arg in i.Args.OfType<ICalcArg>()) arg.Parent = i;
            IEnumerable<byte> bytes = new[] { i.Code };
            if (i.Extra is { } extra) bytes = bytes.Concat(extra.OfType<byte>());
            return bytes.Concat(i.Args.OfType<IByteArg>()
                .SelectMany(a => a.Bytes)).ToArray();
        }
    }
}