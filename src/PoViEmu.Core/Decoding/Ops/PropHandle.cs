using System;
using PoViEmu.Core.Hardware;

namespace PoViEmu.Core.Decoding.Ops
{
    public abstract record PropHandle
    {
        internal MachineState State { get; set; }

        protected abstract object Get();

        public abstract void Set(object value);

        public virtual byte U8() => Convert.ToByte(Get());
        public virtual ushort U16() => Convert.ToUInt16(Get());
        public virtual int I32() => Convert.ToInt32(Get());
    }
}