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
        [SerializeField] private GunSettingsConfig _gunSettings;
        private Timer _timerForReloading = new Timer();
        private Pool<Bullet> _bulletPool;
        private BulletBehaviourFactory _bulletBehaviourFactory;
        
        private const int PooledBullets = 10;

        public void InitWeapon(Transform holster, IBulletBehaviourDatable bulletSetting)
        {
            if (_gunSettings == null)
            {
                Debug.LogError($"Add a settings for gun {this.gameObject.name}");
                return;
            }

            transform.position = holster.position;
            transform.parent = holster;

            _bulletPool = new Pool<Bullet>(new PrefabFactory<Bullet>(_gunSettings.BulletPrefab.gameObject), PooledBullets, null);
            _bulletBehaviourFactory = new BulletBehaviourFactory(bulletSetting);
        }

        public void Fire()
        {
            if (_bulletPool == null)
            {
                return;
            }

            if (_timerForReloading.IsTimerActive.Value)
            {
                return;
            }

            _timerForReloading.StartTimer(_gunSettings.Cooldown);

            Bullet bullet = _bulletPool.Allocate();
            EventHandler handler = null; //TODO Remove anonymous method

            handler = (sender, e) =>
            {
                _bulletPool.Release(bullet);
                bullet.OnDeath -= handler;
            }; 

            bullet.OnDeath += handler;
            bullet.Restore();
            bullet.Init(_gunSettings.Damage, _gunSettings.BulletSpeed, transform.forward);
            bullet.SetBehaviour(_bulletBehaviourFactory.Create());
            bullet.gameObject.transform.position = transform.position;

            bullet.Launch();
        }

        private void OnDestroy()
        {
            _timerForReloading.StopTimer();
        }
    }
}
