using DG.Tweening;
using ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace TestShooter.Shooting.Bullets
{
    public class Bullet : MonoBehaviour, IPoolable, ITouchable
    {
        public event EventHandler OnDeath;
        private float _damage;
        private float _speed;
        private Vector3 _direction;

        private IBulletBehavior _behaviour;

        public float Damage => _damage;
        public float Speed => _speed;

        public void Init(float damage, float speed, Vector3 direction)
        {
            _damage = damage;
            _speed = speed;
            _direction = direction;
        }

        public void Launch()
        {
            if (_behaviour == null)
            {
                _behaviour = new DefaultBullet();
            }
        }

        public void SetBehaviour(IBulletBehavior behaviour)
        {
            _behaviour = behaviour;
        }

        public void OnTriggerEnter(Collider collision)
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            if (damageable == null)
            {
                Die();
                return;
            }

            _behaviour?.ExecuteBehavior(this, collision, damageable);
        }

        private void Update()
        {
            transform.position += _direction * _speed * Time.deltaTime;
        }

        public void Die()
        {
            OnDeath?.Invoke(this, null);
        }

        public void Reset()
        {
            gameObject.SetActive(false);
        }

        public void Restore()
        {
            gameObject.SetActive(true);
        }

        public void PlaceUnderParent(Transform transform)
        {
            this.transform.parent = transform;
        }
    }
}
