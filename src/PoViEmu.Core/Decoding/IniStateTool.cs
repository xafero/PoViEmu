using System.Collections.Generic;
using System.IO;
using System.Text;
using PoViEmu.Core.Hardware;
using Tomlyn;

// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Decoding
{
    public static class IniStateTool
    {
        private class StateModel
        {
            public string AX { get; set; }
            public string BX { get; set; }
            public string CX { get; set; }
            public string DX { get; set; }
            public string SI { get; set; }
            public string DI { get; set; }
            public string DS { get; set; }
            public string ES { get; set; }
            public string SS { get; set; }
            public string SP { get; set; }
            public string BP { get; set; }
            public string CS { get; set; }
            public string IP { get; set; }
            public int CF { get; set; }
            public int ZF { get; set; }
            public int SF { get; set; }
            public int DF { get; set; }
            public int IF { get; set; }
            public int OF { get; set; }
            public int PF { get; set; }
            public int AF { get; set; }
            public string B0 { get; set; }
            public string B1 { get; set; }
            public string B2 { get; set; }
            public string B3 { get; set; }
            public string B4 { get; set; }
            public string B5 { get; set; }
            public string B6 { get; set; }
            public string F0 { get; set; }
            public string F1 { get; set; }
            public string F2 { get; set; }
            public string F3 { get; set; }
            public string F4 { get; set; }
            public string F5 { get; set; }
            public string F6 { get; set; }
            public string F7 { get; set; }
            public string F8 { get; set; }
            public string F9 { get; set; }
            public string F10 { get; set; }
            public string F11 { get; set; }
            public Dictionary<string, string[]> Stack { get; set; }
            public Dictionary<string, Dictionary<string, string[]>> Memory { get; set; }
        }

        private static readonly TomlModelOptions Opt = new()
        {
            ConvertPropertyName = name => name.ToUpperInvariant()
        };

        public static string SerializeState(this MachineState state, string root)
        {
            var model = new StateModel
            {
                AX = state.AX.AsHex(),
                BX = state.BX.AsHex(),
                CX = state.CX.AsHex(),
                DX = state.DX.AsHex(),
                SI = state.SI.AsHex(),
                DI = state.DI.AsHex(),
                DS = state.DS.AsHex(),
                ES = state.ES.AsHex(),
                SS = state.SS.AsHex(),
                SP = state.SP.AsHex(),
                BP = state.BP.AsHex(),
                CS = state.CS.AsHex(),
                IP = state.IP.AsHex(),
                CF = state.CF.AsInt(),
                ZF = state.ZF.AsInt(),
                SF = state.SF.AsInt(),
                DF = state.DF.AsInt(),
                IF = state.IF.AsInt(),
                OF = state.OF.AsInt(),
                PF = state.PF.AsInt(),
                AF = state.AF.AsInt(),
                B0 = state.Bk0.AsHex(),
                B1 = state.Bk1.AsHex(),
                B2 = state.Bk2.AsHex(),
                B3 = state.Bk3.AsHex(),
                B4 = state.Bk4.AsHex(),
                B5 = state.Bk5.AsHex(),
                B6 = state.Bk6.AsHex(),
                F0 = state.Fr0.AsHex(),
                F1 = state.Fr1.AsHex(),
                F2 = state.Fr2.AsHex(),
                F3 = state.Fr3.AsHex(),
                F4 = state.Fr4.AsHex(),
                F5 = state.Fr5.AsHex(),
                F6 = state.Fr6.AsHex(),
                F7 = state.Fr7.AsHex(),
                F8 = state.Fr8.AsHex(),
                F9 = state.Fr9.AsHex(),
                F10 = state.Fr10.AsHex(),
                F11 = state.Fr11.AsHex(),
                // Stack = state.Stack.AsHex(),
                // Memory = state.Memory.AsHex(root)
            };
            var text = Toml.FromModel(model, Opt);
            return text;
        }

        public static MachineState DeserializeState(this string text, string root)
        {
            var model = Toml.ToModel<StateModel>(text, null, Opt);
            var state = new MachineState
            {
                AX = model.AX.AsUShort(),
                BX = model.BX.AsUShort(),
                CX = model.CX.AsUShort(),
                DX = model.DX.AsUShort(),
                SI = model.SI.AsUShort(),
                DI = model.DI.AsUShort(),
                DS = model.DS.AsUShort(),
                ES = model.ES.AsUShort(),
                SS = model.SS.AsUShort(),
                SP = model.SP.AsUShort(),
                BP = model.BP.AsUShort(),
                CS = model.CS.AsUShort(),
                IP = model.IP.AsUShort(),
                CF = model.CF.AsBool(),
                ZF = model.ZF.AsBool(),
                SF = model.SF.AsBool(),
                DF = model.DF.AsBool(),
                IF = model.IF.AsBool(),
                OF = model.OF.AsBool(),
                PF = model.PF.AsBool(),
                AF = model.AF.AsBool(),
                Bk0 = model.B0.AsUShort(),
                Bk1 = model.B1.AsUShort(),
                Bk2 = model.B2.AsUShort(),
                Bk3 = model.B3.AsUShort(),
                Bk4 = model.B4.AsUShort(),
                Bk5 = model.B5.AsUShort(),
                Bk6 = model.B6.AsUShort(),
                Fr0 = model.F0.AsUShort(),
                Fr1 = model.F1.AsUShort(),
                Fr2 = model.F2.AsUShort(),
                Fr3 = model.F3.AsUShort(),
                Fr4 = model.F4.AsUShort(),
                Fr5 = model.F5.AsUShort(),
                Fr6 = model.F6.AsUShort(),
                Fr7 = model.F7.AsUShort(),
                Fr8 = model.F8.AsUShort(),
                Fr9 = model.F9.AsUShort(),
                Fr10 = model.F10.AsUShort(),
                Fr11 = model.F11.AsUShort(),
                // Stack = model.Stack.AsUShort(),
                // Memory = model.Memory.AsUShort(root)
            };
            return state;
        }

        public static MachineState ReadFile(string file)
        {
            var root = Path.GetDirectoryName(file) ?? string.Empty;
            var text = File.ReadAllText(file, Encoding.UTF8);
            var model = DeserializeState(text, root);
            return model;
        }
    }
}