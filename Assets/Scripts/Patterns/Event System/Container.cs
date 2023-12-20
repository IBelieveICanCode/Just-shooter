using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    public class Container<T1>
    {
        private readonly Dictionary<Type, T1> _items = new Dictionary<Type, T1>();

        public void Add(Type key, T1 value)
        {
            if (!_items.ContainsKey(key))
            {
                _items.Add(key, value);
            }
            else
            {
                _items[key] = value;
            }
        }

        public T Get<T>() where T : class, T1
        {
            var type = typeof(T);

            if (_items.ContainsKey(type))
            {
                return _items[type] as T;
            }

            return null;
        }

        public bool Contains(Type key)
        {
            return _items.ContainsKey(key);
        }

        public void Remove(Type key)
        {
            if (_items.ContainsKey(key))
            {
                _items.Remove(key);
            }
        }
    }
}
