using System;
using System.Collections.Generic;
using Iced.Intel;

namespace PoViEmu.Core.Decoding
{
    public static class DecoderExt
    {
        public static Decoder Create16(this CodeReader reader)
        {
            var options = DecoderOptions.NoInvalidCheck |
                          DecoderOptions.OldFpu |
                          DecoderOptions.Loadall286;
            var decoder = Decoder.Create(16, reader, options);
            return decoder;
        }

        public static IEnumerable<XInstruction> Decode(this TrackCodeReader reader)
        {
            var decoder = Create16(reader);
            while (true)
            {
                decoder.Decode(out var instruct);
                if (instruct.Length == 0)
                    break;
                var (off, bytes) = reader.GetReadBytes();
                var byteStr = Convert.ToHexString(bytes).ToLower();
                var item = new XInstruction(off, byteStr, instruct);
                yield return item;
            }
        }

        private static readonly NasmFormatter format = new();
        private static readonly StringOutput output = new();

        public static string Format(this Instruction instruct)
        {
            format.Format(instruct, output);
            return output.ToStringAndReset();
        }
    }
}