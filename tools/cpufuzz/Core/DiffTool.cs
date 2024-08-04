using System.Collections.Generic;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;

namespace PoViEmu.CpuFuzzer.Core
{
    public static class DiffTool
    {
        public static List<string> DiffThis(string first, string second)
        {
            var results = new List<string>();
            var diff = InlineDiffBuilder.Diff(first, second);
            foreach (var line in diff.Lines)
            {
                string prefix;
                switch (line.Type)
                {
                    case ChangeType.Deleted:
                        prefix = "- ";
                        break;
                    case ChangeType.Inserted:
                        prefix = "+ ";
                        break;
                    case ChangeType.Modified:
                        prefix = "~ ";
                        break;
                    case ChangeType.Unchanged:
                    case ChangeType.Imaginary:
                    default:
                        continue;
                }
                results.Add($"{prefix}{line.Text}");
            }
            return results;
        }
    }
}