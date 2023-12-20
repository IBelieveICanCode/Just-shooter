using UnityEngine;

namespace ObjectPool
{
    public class PrefabFactory<T> : IFactory<T> where T : MonoBehaviour
    {
        private GameObject _prefab;
        private string _name;
        private int  _index = 0;

        public PrefabFactory(GameObject prefab) : this(prefab, prefab.name) { }

        public PrefabFactory(GameObject prefab, string name)
        {
            this._prefab = prefab;
            this._name = name;
        }

        public T Create()
        {
            GameObject tempGameObject = GameObject.Instantiate(_prefab) as GameObject;
            tempGameObject.name = _name + _index.ToString();
            //T objectOfType = tempGameObject.TryGetComponent<T>();
            T objectOfType;

            if (!tempGameObject.TryGetComponent<T>(out objectOfType))
            {
                objectOfType = tempGameObject.AddComponent<T>();
            }

            _index++;
            return objectOfType;
        }
    }
}