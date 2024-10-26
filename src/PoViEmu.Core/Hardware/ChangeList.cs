using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using PoViEmu.Common;

namespace PoViEmu.Core.Hardware
{
    public sealed class ChangeList:IDisposable
    {
        private readonly MachineState _m;
        private readonly ConcurrentDictionary<string, object> _v;

        public ChangeList(MachineState m)
        {
            _m = m;
            _v = new ConcurrentDictionary<string, object>();
            _m.PropertyChanging += OnPropertyChanging;
            _m.PropertyChanged  += OnPropertyChanged;
        }
        
        private void OnPropertyChanging(object? sender, PropertyChangingEventArgs e)
        {
            var key = e.PropertyName.TrimNull();
            if (key==null)
                return;
            var state = (MachineState)sender!;
            var oldVal = state[e.PropertyName];
            _v[key] = oldVal;
        }
        
        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            var key = e.PropertyName.TrimNull();
            if (key==null)
                return;
            var state = (MachineState)sender!;
            var newVal = state[e.PropertyName];
            _v.TryGetValue(key, out var oldVal);
            var pair = (oldVal, newVal);
            PropertyChanged?.Invoke(this, new PropertyEventArgs(key, pair));
            _v[key] = pair;
        }
        
        public event PropertyEventHandler? PropertyChanged;
        
        public void Dispose()
        {


            
            foreach (var t in _v)
            {
                Console.WriteLine(t.Key);
                Console.WriteLine(t.Value+" / "+t.Value.GetType());
            }
            
            
            
            
            
            
            
            _v.Clear();
            _m.PropertyChanging -= OnPropertyChanging;
            _m.PropertyChanged  -= OnPropertyChanged;
        }
    }
}