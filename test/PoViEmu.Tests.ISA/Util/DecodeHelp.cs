using System;
using System.IO;

namespace PoViEmu.Tests.ISA.Util
{
    public class DecodeHelp
    {
        internal static string ToLine(int i, string hex, string txt)
        {
            return $" #{i:D5} | {hex} | {txt}";
        }
        
        internal static void ParseAll(ICodeParser parser, StreamWriter writer, LineWrite func)
        {
            using (writer)
            {
                const ushort min = ushort.MinValue;
                const ushort max = ushort.MaxValue;

                for (int i = min; i <= max; i++)
                {
                    var bytes = BitConverter.GetBytes((short)i);
                    (bytes[0], bytes[1]) = (bytes[1], bytes[0]);
                    var hex = Convert.ToHexString(bytes);

                    var codeTxt = parser.Parse(bytes);
                    var debug = func(i, hex, codeTxt);
                    writer.WriteLine(debug);
                }
                writer.Flush();
            }
        }
    }
}