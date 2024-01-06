using System.Collections;
using System.Collections.Generic;
using TestShooter.InputSystem;
using TestShooter.Shooting;
using TestShooter.Shooting.Bullets;
using UnityEngine;

namespace TestShooter.Player
{
    public class PlayerShooter : ICanUseWeaponable
    {
        private Transform _weaponHand;

        private IInputable _inputProvider;
        private IWeaponable _currentWeapon;

        public PlayerShooter(Transform weaponHand, IWeaponable gun, IInputable inputProvider, IBulletBehaviourDatable bulletSetting)
        {
            _weaponHand = weaponHand;
            _currentWeapon = gun;
            _currentWeapon.InitWeapon(_weaponHand, bulletSetting);

            _inputProvider = inputProvider;
            _inputProvider.OnShootDone += UseCurrentWeapon;
        }

        public void UseCurrentWeapon()
        {
            _currentWeapon.Fire();
        }
    }
}
