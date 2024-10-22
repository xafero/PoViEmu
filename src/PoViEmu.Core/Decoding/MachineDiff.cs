using System;
using System.Collections.Generic;
using System.Linq;
using PoViEmu.Core.Hardware;

// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Decoding
{
    public static class MachineDiff
    {
        public interface IDiffItem
        {
        }

        public record DiffItem<T>(string Prefix, T First, T Second) : IDiffItem
        {
            public override string ToString()
            {
                if (First is ushort fUS && Second is ushort sUS)
                    return $"{Prefix} : 0x{fUS:X4} --> 0x{sUS:X4}";

                if (First is bool fBO && Second is bool sBO)
                    return $"{Prefix} : {(fBO ? 1 : 0)} --> {(sBO ? 1 : 0)}";

                if (First is IDictionary<ushort, List<ushort>> fDS &&
                    Second is IDictionary<ushort, List<ushort>> sDS)
                {
                    var fD = string.Join(",", fDS.Keys.Select(k => $"0x{k:X4}"));
                    var sD = string.Join(",", sDS.Keys.Select(k => $"0x{k:X4}"));
                    return $"{Prefix} : ({fD}) --> ({sD})";
                }

                if (First is IDictionary<ushort, IDictionary<ushort, List<byte>>> fDD &&
                    Second is IDictionary<ushort, IDictionary<ushort, List<byte>>> sDD)
                {
                    var fM = string.Join(",", fDD.Keys.Select(k => $"0x{k:X4}"));
                    var sM = string.Join(",", sDD.Keys.Select(k => $"0x{k:X4}"));
                    return $"{Prefix} : ({fM}) --> ({sM})";
                }

                throw new InvalidOperationException($"{First?.GetType()} {Second?.GetType()}");
            }
        }

        public static DiffItem<TP>? IsEqual<TO, TP>(TO firstO, TO secondO,
            Func<TO, TP> func, string prefix)
        {
            var first = func(firstO);
            var second = func(secondO);
            if (first == null && second == null)
                return null;
            if (first != null && first.Equals(second))
                return null;
            return new DiffItem<TP>(prefix, first, second);
        }

        public static IList<IDiffItem> Compare(this MachineState first, MachineState second)
        {
            var items = new List<IDiffItem>();
            if (IsEqual(first, second, i => i.AX, "AX") is { } ax)
                items.Add(ax);
            if (IsEqual(first, second, i => i.BX, "BX") is { } bx)
                items.Add(bx);
            if (IsEqual(first, second, i => i.CX, "CX") is { } cx)
                items.Add(cx);
            if (IsEqual(first, second, i => i.DX, "DX") is { } dx)
                items.Add(dx);
            if (IsEqual(first, second, i => i.SI, "SI") is { } si)
                items.Add(si);
            if (IsEqual(first, second, i => i.DI, "DI") is { } di)
                items.Add(di);
            if (IsEqual(first, second, i => i.DS, "DS") is { } ds)
                items.Add(ds);
            if (IsEqual(first, second, i => i.ES, "ES") is { } es)
                items.Add(es);
            if (IsEqual(first, second, i => i.SS, "SS") is { } ss)
                items.Add(ss);
            if (IsEqual(first, second, i => i.SP, "SP") is { } sp)
                items.Add(sp);
            if (IsEqual(first, second, i => i.BP, "BP") is { } bp)
                items.Add(bp);
            if (IsEqual(first, second, i => i.CS, "CS") is { } cs)
                items.Add(cs);
            if (IsEqual(first, second, i => i.IP, "IP") is { } ip)
                items.Add(ip);
            if (IsEqual(first, second, i => i.CF, "CF") is { } cf)
                items.Add(cf);
            if (IsEqual(first, second, i => i.ZF, "ZF") is { } zf)
                items.Add(zf);
            if (IsEqual(first, second, i => i.SF, "SF") is { } sf)
                items.Add(sf);
            if (IsEqual(first, second, i => i.DF, "DF") is { } df)
                items.Add(df);
            if (IsEqual(first, second, i => i.IF, "IF") is { } @if)
                items.Add(@if);
            if (IsEqual(first, second, i => i.OF, "OF") is { } of)
                items.Add(of);
            if (IsEqual(first, second, i => i.PF, "PF") is { } pf)
                items.Add(pf);
            if (IsEqual(first, second, i => i.AF, "AF") is { } af)
                items.Add(af);
            if (IsEqual(first, second, i => i.Bk0, "B0") is { } b0)
                items.Add(b0);
            if (IsEqual(first, second, i => i.Bk1, "B1") is { } b1)
                items.Add(b1);
            if (IsEqual(first, second, i => i.Bk2, "B2") is { } b2)
                items.Add(b2);
            if (IsEqual(first, second, i => i.Bk3, "B3") is { } b3)
                items.Add(b3);
            if (IsEqual(first, second, i => i.Bk4, "B4") is { } b4)
                items.Add(b4);
            if (IsEqual(first, second, i => i.Bk5, "B5") is { } b5)
                items.Add(b5);
            if (IsEqual(first, second, i => i.Bk6, "B6") is { } b6)
                items.Add(b6);
            if (IsEqual(first, second, i => i.Fr0, "F0") is { } f0)
                items.Add(f0);
            if (IsEqual(first, second, i => i.Fr1, "F1") is { } f1)
                items.Add(f1);
            if (IsEqual(first, second, i => i.Fr2, "F2") is { } f2)
                items.Add(f2);
            if (IsEqual(first, second, i => i.Fr3, "F3") is { } f3)
                items.Add(f3);
            if (IsEqual(first, second, i => i.Fr4, "F4") is { } f4)
                items.Add(f4);
            if (IsEqual(first, second, i => i.Fr5, "F5") is { } f5)
                items.Add(f5);
            if (IsEqual(first, second, i => i.Fr6, "F6") is { } f6)
                items.Add(f6);
            if (IsEqual(first, second, i => i.Fr7, "F7") is { } f7)
                items.Add(f7);
            if (IsEqual(first, second, i => i.Fr8, "F8") is { } f8)
                items.Add(f8);
            if (IsEqual(first, second, i => i.Fr9, "F9") is { } f9)
                items.Add(f9);
            if (IsEqual(first, second, i => i.Fr10, "F10") is { } f10)
                items.Add(f10);
            if (IsEqual(first, second, i => i.Fr11, "F11") is { } f11)
                items.Add(f11);
            // if (IsEqual(first, second, i => i.Stack, "Stack") is { } sta)
            // items.Add(sta);
            // if (IsEqual(first, second, i => i.Memory, "Memory") is { } mem)
            // items.Add(mem);
            return items;
        }

        public static void Init(this MachineState state)
        {
            // state.Stack = new();
            // state.Memory = new();
        }
    }
}