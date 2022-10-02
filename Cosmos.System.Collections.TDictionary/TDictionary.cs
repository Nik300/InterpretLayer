using System;
using System.Collections;
using System.Collections.Generic;

namespace Cosmos.System.Collections
{
    public sealed class TDictionary<Tkey, Tval>
    {
        private List<Tkey> _keys = new();
        private List<Tval> _values = new();

        public IReadOnlyList<Tkey> Keys => _keys;
        public IReadOnlyList<Tval> Values => _values;

        public Tval this[Tkey key]
        {
            get
            {
                var i = _keys.FindIndex((x) => x.Equals(key));
                if (i == -1) throw new($"Property {key} was not found");
                return _values[i];
            }
            set
            {
                var i = _keys.FindIndex((x) => x.Equals(key));
                if (i == -1) throw new($"Property {key} was not found");
                _values[i] = value;
            }
        }

        public void Add(Tkey key, Tval value)
        {
            _keys.Add(key);
            _values.Add(value);
        }
        public void Remove(Tkey key, Tval value)
        {
            _keys.Remove(key);
            _values.Remove(value);
        }
    }
}
