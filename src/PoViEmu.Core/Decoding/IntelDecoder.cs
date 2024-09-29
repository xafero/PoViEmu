using System.IO;
using Iced.Intel;

namespace PoViEmu.Core.Decoding
{
    public static class IntelDecoder
    {
        public static Decoder GetDecoder(out MemoryStream stream)
        {
            stream = new MemoryStream();
            var reader = new StreamCodeReader(stream);
            var decoder = Decoder.Create(16, reader, DecoderOptions.NoInvalidCheck);
            return decoder;
        }

        public static NasmFormatter GetFormatter()
        {
            var options = new FormatterOptions
            {
                BinaryPrefix = "0b",
                BinarySuffix = null,
                HexPrefix = "0x",
                HexSuffix = null,
                SmallHexNumbersInDecimal = false,
                SpaceAfterOperandSeparator = true,
                UppercaseMnemonics = true
            };
            var formatter = new NasmFormatter(options);
            return formatter;
        }
    }
}