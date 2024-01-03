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
        private Vector3 _launchDirection;

        private IBulletBehavior _behaviour;
        private IDisposable _movementSubscription;

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
            _movementSubscription = Observable.EveryUpdate()
            .Subscribe(_ =>
            {
                transform.position += _direction * _speed * Time.deltaTime;
            });

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

            damageable.ReceiveDamage(_damage);
            _behaviour?.ExecuteBehavior(this, collision);
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

        public void Restore()
        {
            gameObject.SetActive(true);
        }

        private void EndMovement()
        {
            if (_movementSubscription != null)
            {
                _movementSubscription.Dispose();
            }
        }

        public void PlaceUnderParent(Transform transform)
        {
            this.transform.parent = transform;
        }
    }
}
