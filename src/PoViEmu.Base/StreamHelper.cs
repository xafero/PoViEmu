using System;
using System.Collections.Generic;
using System.IO;

namespace PoViEmu.Base
{
    public static class StreamHelper
    {
        public static byte[]? CopyToBytes(this Stream? stream)
        {
            if (stream == null)
                return null;
            using (stream)
            {
                using var mem = new MemoryStream();
                stream.CopyTo(mem);
                stream.Flush();
                return mem.ToArray();
            }
        }

        public static IDictionary<string, byte[]> GetManifestResources<T>(string space)
            => GetManifestResources(typeof(T), space);

        public static IDictionary<string, byte[]> GetManifestResources(this Type type, string space)
        {
            var dict = new Dictionary<string, byte[]>();
            var ass = type.Assembly;
            var prefix = $"{type.Namespace}.{space}.";
            var names = ass.GetManifestResourceNames();
            foreach (var name in names)
            {
                if (!name.StartsWith(prefix))
                    continue;
                var tmp = name.Split(prefix, 2);
                if (tmp.Length != 2 || tmp[0].Length != 0)
                    continue;
                var data = ass.GetManifestResourceStream(name).CopyToBytes();
                if (data == null)
                    continue;
                var key = tmp[1];
                dict[key] = data;
            }
            return dict;
        }
    }
}