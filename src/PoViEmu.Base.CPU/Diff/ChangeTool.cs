using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace PoViEmu.Base.CPU.Diff
{
    public static class ChangeTool
    {
        public static ChangeList Collect(this IState m)
        {
            var list = new ChangeList(m);
            return list;
        }

        public static string[] ToChangeLines(this ChangeList list,
            IValFormatter fmt, bool ignoreIP = false)
        {
            var bld = new List<string>();
            using (list)
                foreach (var e in list.Changes)
                {
                    var k = e.PropertyName;
                    if (ignoreIP && k is "IP" or "PC")
                        continue;
                    var t = $"{k} = {fmt.Format(e.Old)} --> {fmt.Format(e.New)}";
                    bld.Add(t);
                }
            return bld.ToArray();
        }
    }
}