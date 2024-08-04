using System;
using System.Linq;
using PoViEmu.Common;
using PoViEmu.Core.Machine.Args;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.Core.Machine.Core
{
    public class Instruction
    {
        public Instruction(long pos, byte code, int size, OpCode op, Modifier? mod = null, OpArg[]? args = null)
        {
            Offset = (int)pos;
            Bytes = [code];
            Size = size;
            Op = op;
            Mod = mod;
            Args = args;
            if (args == null)
                return;
            foreach (var arg in args.OfType<ICalcArg>()) arg.Parent = this;
            Bytes = Bytes.Concat(args.OfType<IByteArg>().SelectMany(a => a.Bytes)).ToArray();
        }

        public int Offset { get; set; }
        public byte[] Bytes { get; set; }
        public int Size { get; set; }
        public OpCode? Op { get; set; }
        public Modifier? Mod { get; set; }
        public OpArg[]? Args { get; set; }

        public override string ToString()
        {
            var hex = Convert.ToHexString(Bytes);
            var space = TextHelper.Space(18 - hex.Length);
            var op = Op?.ToString();
            if (Mod != null) op += $" {Mod}";
            var reg = string.Join(",", (Args ?? []).Select(a => a?.ToString()));
            var txt = $"{Offset:X8}  {hex}{space}{op} {reg}";
            return txt.Trim();
        }
    }
}