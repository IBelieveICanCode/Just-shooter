using ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using TestShooter.Shooting.Bullets;
using TestShooter.Timers;
using UnityEngine;

namespace TestShooter.Shooting
{
    public class DefaultGun : MonoBehaviour, IWeaponable
    {
        [SerializeField] private float _cooldown = 3;
        [SerializeField] private float _damage = 2;
        [SerializeField] private float _bulletSpeed = 2;
        [SerializeField] private Bullet _bulletPrefab;

        private Timer _timerForReloading;
        private int _pooledBullets = 10;
        private Pool<Bullet> _bulletPool;
        private BulletBehaviourFactory _bulletBehaviourFactory;

        private void Start()
        {
            _bulletPool = new Pool<Bullet>(new PrefabFactory<Bullet>(_bulletPrefab.gameObject), _pooledBullets, null);
            _timerForReloading = new Timer();
        }

        public void InitWeapon(Transform holster, IBulletSettingable bulletSetting)
        {
            transform.position = holster.position;
            transform.parent = holster;
            
            _bulletBehaviourFactory = new BulletBehaviourFactory(bulletSetting);
        }

        public void Fire()
        {
            if (_timerForReloading.IsTimerActive.Value)
            {
                return;
            }

            _timerForReloading.StartTimer(_cooldown);

            Bullet bullet = _bulletPool.Allocate();
            EventHandler handler = null; //TODO Remove anonymous method

            handler = (sender, e) =>
            {
                _bulletPool.Release(bullet);
                bullet.OnDeath -= handler;
            }; 

            bullet.OnDeath += handler;
            bullet.Restore();
            bullet.Init(_damage, _bulletSpeed, transform.forward);
            bullet.SetBehaviour(_bulletBehaviourFactory.Create());
            bullet.gameObject.transform.position = transform.position;

            bullet.Launch();
        }
    }
}
