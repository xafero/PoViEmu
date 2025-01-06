using System;
using System.Collections.Generic;
using PoViEmu.Base;

namespace PoViEmu.Tests.ISA.Util
{
    internal sealed class JsonParser : ICodeParser
    {
        private readonly IDictionary<string, string> _dict;

        public JsonParser(string file)
        {
            _dict = JsonHelper.ReadFromFile<IDictionary<string, string>>(file);
        }

        public string Parse(byte[] bytes)
        {
            var hex = Convert.ToHexString(bytes);
            var text = _dict[hex];
            return text;
        }
    }
}