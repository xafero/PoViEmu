using System;
using PoViEmu.Base.ISA;

namespace PoViEmu.Workbook
{
    internal sealed class FailedInstr : IInstruction
    {
        private readonly Exception _error;

        public FailedInstr(Exception error)
        {
            _error = error;
        }

        public override string ToString()
        {
            var codeTxt = _error.GetType().Name.Replace("Exception", "");
            const string space = "   ";
            var arg = _error.Message;
            return $"0{space}{codeTxt}{space}{arg}".Trim();
        }
    }
}