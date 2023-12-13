using ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Shooting
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour, IResettable
    {
        public event EventHandler OnDeath;
        private float _damage;
        private float _speed;
        private Rigidbody _rigidBody;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        public void Init(float damage, float speed, Vector3 direction)
        {
            _damage = damage;
            _speed = speed;
            _rigidBody.velocity = (direction * _speed);
        }

        private void OnCollisionEnter(Collision collision)
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            if (damageable == null)
            {
                Die();
                return;
            }

            damageable.ReceiveDamage(_damage);
            Die();
        }

        private void Die()
        {
            OnDeath?.Invoke(this, null);
        }

        public void Reset()
        {
            gameObject.SetActive(false);
        }


    }
}
