using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
	public class Pool<T> : IEnumerable where T : IPoolable
	{
		private Transform _poolParent;

		private List<T> _members = new List<T>();
		private HashSet<T> _unavailable = new HashSet<T>();
		private IFactory<T> _factory;

		private const int DefaultPoolSize = 5;

		public Pool(IFactory<T> factory, int poolSize, Transform parent)
		{
			_factory = factory;
			_poolParent = parent;

			for (int i = 0; i < poolSize; i++)
			{
				Create();
			}
		}

		public T Allocate()
		{
			for (int i = 0; i < _members.Count; i++)
			{
				if (!_unavailable.Contains(_members[i]))
				{
					_unavailable.Add(_members[i]);
					return _members[i];
				}
			}

			T newMember = Create();
			_unavailable.Add(newMember);
			return newMember;
		}

		T Create()
		{
			T member = _factory.Create();
			member.Reset();
			_members.Add(member);
			member.PlaceUnderParent(_poolParent);
			return member;
		}

		public void Release(T member)
		{
			member.Reset();
			_unavailable.Remove(member);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _members.GetEnumerator();
		}
	}
}