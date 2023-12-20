using UnityEngine;

namespace ObjectPool
{
    public interface IPoolable
    {
        void PlaceUnderParent(Transform parent);
        void Reset();
        void Restore();
    }
}