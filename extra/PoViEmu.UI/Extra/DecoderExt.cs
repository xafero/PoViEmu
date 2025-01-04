using System;
using System.Collections.Generic;
using PoViEmu.I186.ISA.Decoding;
using DTx = PoViEmu.I186.ISA.Decoding.DecoderExt;

namespace PoViEmu.UI.Extra
{
    public static class DecoderExt
    {
        public static IEnumerable<XInstruction> Decode(this TrackCodeReader reader, ushort ip)
        {
            var decoder = DTx.Create16(reader, ip);
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
    }
}