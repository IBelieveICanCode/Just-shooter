using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public class MultiplePrefabFactory<T> : IFactoryType<T> where T : MonoBehaviour
    {
        private Dictionary<Type, GameObject> _prefabs;
        private string _name;
        private int _index = 0;

        public MultiplePrefabFactory(Dictionary<Type, GameObject> prefabs, string name)
        {
            this._prefabs = prefabs;
            this._name = name;
        }

        public T Create(Type type)
        {
            if (!_prefabs.TryGetValue(type, out GameObject prefab))
            {
                throw new InvalidOperationException("Prefab not registered for type " + type);
            }

            GameObject tempGameObject = GameObject.Instantiate(prefab) as GameObject;
            tempGameObject.name = _name + _index.ToString();

            if (!tempGameObject.TryGetComponent<T>(out T objectOfType))
            {
                objectOfType = tempGameObject.AddComponent<T>();
            }

            _index++;
            return objectOfType;
        }

        public T Create()
        {
            return null;
        }
    }
}
