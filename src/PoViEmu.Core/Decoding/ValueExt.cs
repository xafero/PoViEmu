using System;
using System.Collections.Generic;
using System.Linq;

namespace PoViEmu.Core.Decoding
{
    public static class ValueExt
    {
        public static bool AsBool(this int value)
            => value switch
            {
                0 => false,
                1 => true,
                _ => throw new ArgumentException($"{value} not supported!")
            };

        public static int AsInt(this bool value)
            => value switch
            {
                false => 0,
                true => 1
            };

        public static string AsHex(this ushort value)
            => $"0x{value:x4}";

        public static string AsHex(this byte value)
            => $"0x{value:x2}";

        public static byte[] AsBytes(this string hex)
            => Convert.FromHexString(hex.Replace("0x", "")).Reverse().ToArray();

        public static byte AsByte(this string hex)
            => hex.AsBytes().Single();

        public static ushort AsUShort(this string hex)
            => BitConverter.ToUInt16(hex.AsBytes());

        public static Dictionary<string, string[]> AsHex(this IDictionary<ushort, List<ushort>> d)
            => d.ToDictionary(k => k.Key.AsHex(),
                v => v.Value.Select(i => i.AsHex()).ToArray());

        public static Dictionary<ushort, List<ushort>> AsUShort(this IDictionary<string, string[]> d)
            => d.ToDictionary(k => k.Key.AsUShort(),
                v => v.Value.Select(i => i.AsUShort()).ToList());

        public static Dictionary<ushort, Dictionary<ushort, IMemBlob>> AsUShort(this
            Dictionary<string, Dictionary<string, string[]>> d)
            => d.ToDictionary(k => k.Key.AsUShort(),
                v => v.Value.ToDictionary(w => w.Key.AsUShort(),
                    u => FromStringArray(u.Value)));

        public static Dictionary<string, Dictionary<string, string[]>> AsHex(this
            Dictionary<ushort, Dictionary<ushort, IMemBlob>> d)
            => d.ToDictionary(k => k.Key.AsHex(),
                v => v.Value.ToDictionary(w => w.Key.AsHex(),
                    u => ToStringArray(u.Value)));

        private static string[] ToStringArray(IMemBlob obj)
        {
            if (obj is MemFile mf)
            {
                var path = mf.Path;
                var offset = mf.Offset;
                return [path, $"{offset}"];
            }
            var bytes = obj.GetBytes();
            return bytes.Select(i => i.AsHex()).ToArray();
        }

        private static IMemBlob FromStringArray(string[] array)
        {
            if (array.Length == 2 && !array[0].StartsWith("0x"))
            {
                var path = array[0];
                var offset = int.Parse(array[1]);
                return new MemFile(path, offset);
            }
            var bytes = array.Select(i => i.AsByte());
            return new MemList(bytes.ToList());
        }
    }
}