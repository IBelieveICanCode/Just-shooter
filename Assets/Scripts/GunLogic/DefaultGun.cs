using ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Shooting
{
    public class DefaultGun : MonoBehaviour, IGunable
    {
        [SerializeField] private float _damage = 10;
        [SerializeField] private float _bulletSpeed = 10;
        [SerializeField] private Bullet _bulletPrefab;

        private int _pooledBullets = 10;
        private Pool<Bullet> _projPool;

        private void Start()
        {
            _projPool = new Pool<Bullet>(new PrefabFactory<Bullet>(_bulletPrefab.gameObject), _pooledBullets);        
        }

        public void InitGun(Transform holster)
        {
            transform.position = holster.position;
            transform.parent = holster;
        }

        public void Fire()
        {
            Bullet bullet = _projPool.Allocate();
            EventHandler handler = null;

            handler = (sender, e) =>
            {
                _projPool.Release(bullet);
                bullet.OnDeath -= handler;
            };

            bullet.OnDeath += handler;
            bullet.gameObject.SetActive(true);
            bullet.Init(_damage, _bulletSpeed, transform.forward);
            bullet.gameObject.transform.position = transform.position;
        }
    }
}
