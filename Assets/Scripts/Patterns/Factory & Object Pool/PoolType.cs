using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public class PoolType<T> : IEnumerable where T : IPoolable
    {
        private Transform _poolParent;

        private List<T> _members = new List<T>();
        private HashSet<T> _unavailable = new HashSet<T>();
        private IFactoryType<T> _factory;

        public PoolType(IFactoryType<T> factory, Dictionary<Type, int> typeCounts, Transform parent)
        {
            _factory = factory;
            _poolParent = parent;

            foreach (var typeCount in typeCounts)
            {
                Type type = typeCount.Key;
                int count = typeCount.Value;

                for (int i = 0; i < count; i++)
                {
                    Create(type);
                }
            }
        }

        public T Allocate(Type type)
        {
            for (int i = 0; i < _members.Count; i++)
            {
                if (!_unavailable.Contains(_members[i]) && _members[i].GetType() == type)
                {
                    _unavailable.Add(_members[i]);
                    return _members[i];
                }
            }

            T newMember = Create(typeof(T));
            _unavailable.Add(newMember);
            return newMember;
        }

        public void Release(T member)
        {
            member.Reset();
            _unavailable.Remove(member);
        }

        private T Create(Type type)
        {
            T member = _factory.Create(type);
            member.Reset();
            _members.Add(member);
            member.PlaceUnderParent(_poolParent);
            return member;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _members.GetEnumerator();
        }
    }
}