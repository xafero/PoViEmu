using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PoViEmu.Base;

namespace PoViEmu.Inventory.Utils
{
    public sealed class DirDict<T> : IEnumerable<KeyValuePair<string, T>>
    {
        private readonly string _root;
        private readonly string _fileName;

        public DirDict(string root, string fileName)
        {
            _root = Directory.CreateDirectory(root).FullName;
            _fileName = fileName;
        }

        private DirectoryInfo GetDirByKey(string key)
            => new(Path.Combine(_root, key));

        public bool ContainsKey(string key)
            => GetDirByKey(key).Exists;

        public ICollection<string> Keys
            => GetDirByKey("").GetDirectories().Select(d => d.Name).ToArray();

        public ICollection<T> Values
            => Keys.Select(k => this[k]).ToArray();

        public IEnumerator<KeyValuePair<string, T>> GetEnumerator()
            => Keys.Select(k => new KeyValuePair<string, T>(k, this[k])).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public void Add(KeyValuePair<Guid, T> item)
            => Add(item.Key, item.Value);

        public void Add(Guid key, T value)
            => Add(ToKey(key), value);

        public void Add(KeyValuePair<string, T> item)
            => Add(item.Key, item.Value);

        public void Add(string key, T value)
            => this[key] = value;

        public int Count
            => Keys.Count;

        public bool TryGetValue(string key, out T? value)
        {
            if (ContainsKey(key))
            {
                value = this[key];
                return true;
            }
            value = default;
            return false;
        }

        public bool Remove(string key)
        {
            if (GetDirByKey(key) is not { Exists: true } dir)
                return false;
            dir.Delete(recursive: true);
            return true;
        }

        public T this[string key]
        {
            get
            {
                var dir = GetDirByKey(key).FullName;
                var file = Path.Combine(dir, _fileName);
                var text = File.ReadAllText(file, Encoding.UTF8);
                return JsonHelper.FromJson<T>(text);
            }
            set
            {
                var dir = GetDirByKey(key).FullName;
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                var file = Path.Combine(dir, _fileName);
                var text = value == null ? string.Empty : JsonHelper.ToJson(value);
                File.WriteAllText(file, text, Encoding.UTF8);
            }
        }

        private static string ToKey(Guid id)
            => id.ToString("N")[..16];

        public bool ContainsKey(Guid key)
            => ContainsKey(ToKey(key));

        public bool Remove(Guid key)
            => Remove(ToKey(key));

        public bool TryGetValue(Guid key, out T? value)
            => TryGetValue(ToKey(key), out value);

        public T this[Guid id]
        {
            get => this[ToKey(id)];
            set => this[ToKey(id)] = value;
        }
    }
}