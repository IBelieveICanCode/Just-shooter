using System;
using System.Collections;
using System.Collections.Generic;

namespace ObjectPool
{
	public class Pool<T> : IEnumerable where T : IResettable
	{
		private List<T> _members = new List<T>();
		private HashSet<T> _unavailable = new HashSet<T>();
		private IFactory<T> _factory;

		private const int DefaultPoolSize = 5;

		public Pool(IFactory<T> factory) : this(factory, DefaultPoolSize) { }

		public Pool(IFactory<T> factory, int poolSize)
		{
			this._factory = factory;

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

		public void Release(T member)
		{
			member.Reset();
			_unavailable.Remove(member);
		}

		T Create()
		{
			T member = _factory.Create();
			_members.Add(member);
			return member;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _members.GetEnumerator();
		}
	}
}