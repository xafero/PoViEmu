using System;
using System.Collections.Generic;
using Iced.Intel;

namespace PoViEmu.Core.Decoding
{
    public static class DecoderExt
    {
        public static Decoder Create16(this CodeReader reader, ushort ip)
        {
            const DecoderOptions options = DecoderOptions.NoInvalidCheck |
                                           DecoderOptions.OldFpu |
                                           DecoderOptions.Loadall286;
            var decoder = Decoder.Create(16, reader, ip, options);
            return decoder;
        }

        public static IEnumerable<XInstruction> Decode(this TrackCodeReader reader, ushort ip)
        {
            var decoder = Create16(reader, ip);
            while (true)
            {
                decoder.Decode(out var instruct);
                if (instruct.Length == 0)
                    break;
                var (_, bytes) = reader.GetReadBytes();
                var byteStr = Convert.ToHexString(bytes).ToLower();
                var item = new XInstruction(byteStr, instruct);
                yield return item;
            }
        }

        private static readonly StringOutput Output = new();

        private static readonly NasmFormatter Format = new(new FormatterOptions
        {
            UppercaseMnemonics = true
        });

        public static (string op, string arg) FormatStr(this Instruction instruct)
        {
            Format.Format(instruct, Output);
            var text = Output.ToStringAndReset();
            var parts = text.Split(' ', 2);
            var op = instruct.IsInvalid ? "???" : parts[0];
            var arg = parts.Length == 2 ? parts[1] : string.Empty;
            return (op, arg);
        }
    }
}