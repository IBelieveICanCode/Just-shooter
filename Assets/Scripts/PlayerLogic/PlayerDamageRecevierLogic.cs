using System.Collections;
using System.Collections.Generic;
using TestShooter.Shooting;
using UnityEngine;

namespace TestShooter.Player
{
    [RequireComponent(typeof(Collider))]
    public class PlayerDamageRecevierLogic : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _health = 100;
        private float _maxHealth;
        public float Health => _health;

        public void Init(float maxHealth)
        {
            _maxHealth = maxHealth;
        }

        public void Die()
        {
        }

        public void ReceiveDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Die(); 
            }
        }       
    }
}
