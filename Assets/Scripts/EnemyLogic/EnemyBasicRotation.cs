using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter
{
    public class EnemyBasicRotation : IRotatable
    {
        private Transform _ownerTransform;

        public EnemyBasicRotation(Transform ownerTransform)
        {
            _ownerTransform = ownerTransform;
        }
        public void Rotate(Vector3 finalPosition)
        {
            Vector3 direction = finalPosition - _ownerTransform.position;
            direction.y = 0;
            Quaternion newRotation = Quaternion.LookRotation(direction);
            _ownerTransform.rotation = newRotation;
        }
    }
}
