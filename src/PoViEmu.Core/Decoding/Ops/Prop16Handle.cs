using System;
using PoViEmu.Core.Hardware;

namespace PoViEmu.Core.Decoding.Ops
{
    public record Prop16Handle(Func<MachineState, ushort> Getter, Action<MachineState, ushort> Setter)
        : PropHandle
    {
        protected override object Get()
        {
            var val = Getter(State);
            return val;
        }

        public override void Set(object value)
        {
            if (value is ushort u16)
            {
                Setter(State, u16);
                return;
            }
            if (value is int i32)
            {
                Setter(State, (ushort)i32);
                return;
            }
            throw new InvalidOperationException($"{value} | {value.GetType()}");
        }
    }
}