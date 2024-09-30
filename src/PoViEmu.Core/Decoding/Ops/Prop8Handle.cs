using System;
using PoViEmu.Core.Hardware;

namespace PoViEmu.Core.Decoding.Ops
{
    public record Prop8Handle(Func<MachineState, byte> Getter, Action<MachineState, byte> Setter)
        : PropHandle
    {
        protected override object Get()
        {
            var val = Getter(State);
            return val;
        }

        public override void Set(object value)
        {
            if (value is byte u8)
            {
                Setter(State, u8);
                return;
            }
            throw new InvalidOperationException($"{value} | {value.GetType()}");
        }
    }
}