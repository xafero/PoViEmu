using System;
using System.IO;
using System.Linq;
using System.Text;
using Iced.Intel;
using PoViEmu.Core.Hardware;
using Tomlyn;
using Tomlyn.Model;

// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Decoding
{
    public static class IniStateUtil
    {
        private static (Type PropType, string PropName)[] GetStateProperties()
        {
            string[] nonsense = ["Item", "TopOfStack"];
            return typeof(MachineState).GetProperties()
                .Where(p => !nonsense.Contains(p.Name) &&
                            p.PropertyType.Name != "MemAccess`1")
                .Select(p => (p.PropertyType, p.Name))
                .ToArray();
        }

        private static readonly Lazy<(Type PropType, string PropName)[]> StateProps
            = new(GetStateProperties);

        public static void WriteFile(this MachineState m, string file)
        {
            var root = Path.GetDirectoryName(file) ?? string.Empty;
            var text = Serialize(m, root);
            File.WriteAllText(file, text, Encoding.UTF8);
        }

        private static string Serialize(this MachineState m, string root)
        {
            var table = new TomlTable();

            var section = new TomlTable();
            table.Add(nameof(Register), section);

            foreach (var prop in StateProps.Value)
            {
                var propName = prop.PropName;
                var propVal = m[propName];
                section[propName] = propVal.Format();
            }

            return Toml.FromModel(table);
        }

        public static void ReadFile(this MachineState m, string file)
        {
            var root = Path.GetDirectoryName(file) ?? string.Empty;
            var text = File.ReadAllText(file, Encoding.UTF8);
            Deserialize(m, text, root);
        }

        private static void Deserialize(this MachineState m, string text, string root)
        {
            var table = Toml.ToModel(text);

            table.TryGetValue(nameof(Register), out var sec);
            var rSection = (TomlTable)sec;

            foreach (var prop in StateProps.Value)
            {
                var propName = prop.PropName;
                if (!rSection.TryGetValue(propName, out var propVal))
                    continue;
                m[propName] = propVal.Parse(prop.PropType);
            }

            table.TryGetValue("Memory", out var mec);
            var mSection = (TomlTable)mec;

            mSection.TryGetValue(nameof(MachineState.U8), out var u8);
            var u8Section = (TomlTable)u8;
            GoIntoMemorySection<byte>(u8Section, (s, o, a) => m.U8A[s, o] = a);

            mSection.TryGetValue(nameof(MachineState.U16), out var u16);
            var u16Section = (TomlTable)u16;
            GoIntoMemorySection<ushort>(u16Section, (s, o, a) => m.U16A[s, o] = a);
        }

        private static void GoIntoMemorySection<T>(TomlTable section,
            Action<ushort, ushort, T[]> onDone)
        {
            foreach (var prop in section)
            {
                var seg = (ushort)prop.Key.Parse(typeof(ushort));
                if (prop.Value is TomlTable content)
                {
                    foreach (var subProp in content)
                    {
                        var off = (ushort)subProp.Key.Parse(typeof(ushort));
                        if (subProp.Value is TomlArray array)
                        {
                            var texts = array.Cast<string>().ToArray();
                            var res = new T[texts.Length];
                            for (var i = 0; i < res.Length; i++)
                                res[i] = (T)texts[i].Parse(typeof(T));
                            onDone(seg, off, res);
                        }
                    }
                }
            }
        }
    }
}