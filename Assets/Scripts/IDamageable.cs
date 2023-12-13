using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter
{
    public interface IDamageable
    {
        float Health { get; }
        void ReceiveDamage(float damage);
        void Die();
    }
}
