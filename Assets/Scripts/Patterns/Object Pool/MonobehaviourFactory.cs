using UnityEngine;

namespace ObjectPool
{
    public class MonoBehaviourFactory<T> : IFactory<T> where T : MonoBehaviour
    {
        private string _name;
        private int _index = 0;

        public MonoBehaviourFactory() : this("MonoBehaviour") { }

        public MonoBehaviourFactory(string name)
        {
            this._name = name;
        }

        public T Create()
        {
            GameObject tempGameObject = GameObject.Instantiate(new GameObject()) as GameObject;

            tempGameObject.name = _name + _index.ToString();
            tempGameObject.AddComponent<T>();
            T objectOfType = tempGameObject.GetComponent<T>();
            _index++;
            return objectOfType;
        }
    }
}