using System;
using System.Collections.Generic;
using System.IO;
using HexIO;
using static PoViEmu.Common.MathHelper;
using static PoViEmu.Core.Binaries.IntelHexExt;

namespace PoViEmu.Core.Binaries
{
    public static class IntelHexReader
    {
        public static byte[] Extract(Stream input)
        {
            using var output = new MemoryStream();
            Extract(input, output);
            return output.ToArray();
        }

        public static void Extract(Stream input, Stream output)
        {
            using var reader = new IntelHexStreamReader(input);
            var state = new HexState();
            var byteList = new Dictionary<int, byte>();
            var dupList = new Dictionary<int, byte>();
            var minAddr = int.MaxValue;
            var maxAddr = int.MinValue;
            foreach (var record in reader.ReadRecords())
                ReadRecord(record, state, (key, val) =>
                {
                    minAddr = Math.Min(minAddr, key);
                    maxAddr = Math.Max(maxAddr, key);
                    if (!byteList.TryAdd(key, val))
                        if (!dupList.TryAdd(key, val))
                            throw new InvalidOperationException($"Double overwrite at {key}?!");
                });

            var totalSize = (maxAddr + 1) - minAddr;
            var isProLonged = false;
            if (dupList.Count == 2 && !IsPowerOfTwo(totalSize))
            {
                isProLonged = true;
                totalSize = CorrectToNearestPowerOf2(totalSize);
            }

            const byte gapFill = 0xFF;
            var bytes = new byte[totalSize];
            for (var i = 0; i < bytes.Length; i++)
                bytes[i] = byteList.GetValueOrDefault(minAddr + i, gapFill);

            output.Write(bytes, 0, bytes.Length);
            output.Flush();

            if (isProLonged)
                foreach (var item in dupList)
                    output.WriteByte(item.Value);
        }
    }
}