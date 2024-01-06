using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Teleport
{
    [RequireComponent(typeof(Collider))]
    public class TeleportCollider : MonoBehaviour
    {
        public event Action<Collider> OnTeleportColliderEnter;

        private void OnTriggerEnter(Collider other)
        {
            OnTeleportColliderEnter?.Invoke(other);
        }
    }
}
