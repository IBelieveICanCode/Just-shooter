using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter
{
    public interface IDamageable
    {
        event Action OnDeath;
        float Health { get; }
        void InitHealth(IHealthOperatorable healthOperator);
        void ReceiveDamage(float damage);
        void Die();
    }
}
