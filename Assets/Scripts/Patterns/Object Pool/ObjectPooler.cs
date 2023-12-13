using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    [System.Serializable]
    public class ObjectPoolItem
    {
        public int AmountToPool;
        public GameObject ObjectToPool;
        public bool ShouldExpand;
    }

    public class ObjectPooler : Singleton<ObjectPooler>
    {
        public List<ObjectPoolItem> ItemsToPool;
        public List<GameObject> PooledObjects;

        private void Start()
        {
            PooledObjects = new List<GameObject>();
            foreach (ObjectPoolItem item in ItemsToPool)
            {
                for (int i = 0; i < item.AmountToPool; i++)
                {
                    GameObject obj = (GameObject)Instantiate(item.ObjectToPool);
                    obj.SetActive(false);
                    PooledObjects.Add(obj);
                }
            }
        }

        public GameObject GetPooledObject(string tag)
        {
            for (int i = 0; i < PooledObjects.Count; i++)
            {
                if (!PooledObjects[i].activeInHierarchy && PooledObjects[i].tag == tag)
                    return PooledObjects[i];
            }
            foreach (ObjectPoolItem item in ItemsToPool)
            {
                if (item.ObjectToPool.tag == tag)
                {
                    if (item.ShouldExpand)
                    {
                        GameObject obj = (GameObject)Instantiate(item.ObjectToPool);
                        obj.SetActive(false);
                        PooledObjects.Add(obj);
                        return obj;
                    }
                }
            }
            return null;
        }
    }
}
