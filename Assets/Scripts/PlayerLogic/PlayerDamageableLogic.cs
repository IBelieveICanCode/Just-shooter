using System.Collections;
using System.Collections.Generic;
using TestShooter.Shooting;
using UnityEngine;

namespace TestShooter.Player
{
    public class PlayerDamageableLogic : IDamageable
    {
        private float _health;
        private float _maxHealth;
        public float Health => _health;

        public PlayerDamageableLogic(float maxHealth)
        {
            _maxHealth = maxHealth;
        }


        public void Die()
        {

        }

        public void ReceiveDamage(float damage)
        {

        }

        
    }
}
