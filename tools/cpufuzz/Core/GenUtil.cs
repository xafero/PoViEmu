using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PoViEmu.Common;
using PoViEmu.Core.Machine;
using PoViEmu.CpuFuzzer.Core;
using SortedOps = System.Collections.Generic.SortedDictionary<PoViEmu.Core.Machine.Ops.OpCode,
    System.Collections.Generic.SortedDictionary<string, 
        System.Collections.Generic.HashSet<PoViEmu.CpuFuzzer.Core.NasmLine>>>;

namespace PoViEmu.CpuFuzzer.Core
{
    public static class GenUtil
    {
        public static IEnumerable<NasmLine> Iter(SortedOps dict)
        {
            foreach (var (_, b) in dict)
            foreach (var (_, d) in b)
            foreach (var l in d)
                yield return l;
        }
    }
}