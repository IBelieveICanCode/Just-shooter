using DG.Tweening;
using ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace TestShooter.Shooting.Bullets
{
    public class Bullet : MonoBehaviour, IResettable
    {
        public event EventHandler OnDeath;
        private float _damage;
        private float _speed;
        private Vector3 _direction;

        private IBulletBehavior _behavior;
        private IDisposable _movementSubscription;

        public void Init(float damage, float speed, Vector3 direction)
        {
            _damage = damage;
            _speed = speed;
            _direction = direction;
        }

        public void Launch()
        {
            _movementSubscription = Observable.EveryUpdate()
            .Subscribe(_ =>
            {
                transform.position += _direction.normalized * _speed * Time.deltaTime;
            });

            if (_behavior == null)
            {
                _behavior = new DefaultBullet();
            }
        }

        public void SetBehaviour(IBulletBehavior behaviour)
        {
            _behavior = behaviour;
        }


        private void OnTriggerEnter(Collider collision)
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            if (damageable == null)
            {
                Die();
                return;
            }

            damageable.ReceiveDamage(_damage);
            _behavior?.ExecuteBehavior(this, collision);
        }

        public void Die()
        {
            EndMovement();
            OnDeath?.Invoke(this, null);
        }

        public void Reset()
        {
            EndMovement();
            gameObject.SetActive(false);
        }

        private void EndMovement()
        {
            if (_movementSubscription != null)
            {
                _movementSubscription.Dispose();
            }
        }
    }
}
