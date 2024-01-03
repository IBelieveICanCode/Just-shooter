using System;
using System.Collections;
using System.Collections.Generic;
using TestShooter.Shooting;
using UnityEngine;

namespace TestShooter.Player
{
    [RequireComponent(typeof(Collider))]
    public class StandartDamageRecevier : MonoBehaviour, IDamageable
    {
        public event Action OnDeath;

        private IHealthOperatorable _healthOperator;
        public float Health => _healthOperator.Health;

        public void InitHealth(IHealthOperatorable healthOperator)
        {
            _healthOperator = healthOperator;
        }

        public void Die()
        {
            OnDeath?.Invoke();
        }

        public void ReceiveDamage(float damage)
        {
            _healthOperator.SubstractHealth(damage);

            if (_healthOperator.Health <= 0)
            {
                Die(); 
            }
        }       
    }
}
