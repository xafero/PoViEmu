using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace PoViEmu.Base.CPU.Diff
{
    public sealed class ChangeList : IDisposable
    {
        private readonly IState _m;
        private readonly ConcurrentDictionary<string, object> _v;
        private readonly List<PropertyEventArgs> _h;

        public ChangeList(IState m)
        {
            _m = m;
            _v = new ConcurrentDictionary<string, object>();
            _h = [];
            _m.PropertyChanging += OnPropertyChanging;
            _m.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanging(object? sender, PropertyChangingEventArgs e)
        {
            var key = e.PropertyName.TrimNull();
            if (key == null)
                return;
            var state = (IState)sender!;
            var oldVal = state[e.PropertyName];
            _v[key] = oldVal;
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            var key = e.PropertyName.TrimNull();
            if (key == null)
                return;
            var state = (IState)sender!;
            var newVal = state[e.PropertyName];
            _v.TryGetValue(key, out var oldVal);
            var evt = new PropertyEventArgs(key, (oldVal, newVal));
            PropertyChanged?.Invoke(this, evt);
            _h.Add(evt);
            _v[key] = evt;
        }

        public event PropertyEventHandler? PropertyChanged;

        public PropertyEventArgs[] Changes => _h.ToArray();

        [JsonIgnore] public IState State => _m;

        public void Dispose()
        {
            _h.Clear();
            _v.Clear();
            _m.PropertyChanging -= OnPropertyChanging;
            _m.PropertyChanged -= OnPropertyChanged;
        }
    }
}