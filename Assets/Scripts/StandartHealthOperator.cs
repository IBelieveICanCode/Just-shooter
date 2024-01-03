using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter
{
    public class StandartHealthOperator : IHealthOperatorable
    {
        private float _health;
        private float _maxHealth;

        public event Action<float> OnHealthChanged;
        public float Health => _health;
        public float MaxHealth => _maxHealth;

        public StandartHealthOperator(IHealthDatable healthData)
        {
            _maxHealth = healthData.MaxHealth;
            _health = Mathf.Clamp(healthData.StartingHealth, 0, _maxHealth);
            OnHealthChanged?.Invoke(_health);
        }

        public void AddHealth(float amount)
        {
            _health += amount;
            _health = Mathf.Clamp(_health, 0, _maxHealth);
            OnHealthChanged?.Invoke(_health);
        }

        public void SubstractHealth(float amount)
        {
            _health -= amount;
            _health = Mathf.Clamp(_health, 0, _maxHealth);
            OnHealthChanged?.Invoke(_health);
        }

        public void UpdateHealth()
        {
            OnHealthChanged?.Invoke(_health);
        }
    }
}