using System.Collections;
using System.Collections.Generic;
using TestShooter.InputSystem;
using TestShooter.Shooting;
using UnityEngine;

namespace TestShooter.Player
{
    public class PlayerShootingLogic : ICanUseWeaponable
    {
        private Transform _weaponHand;

        private IInputable _inputProvider;
        private IWeaponable _currentWeapon;

        public PlayerShootingLogic(Transform weaponHand, IWeaponable gun, IInputable inputProvider)
        {
            _weaponHand = weaponHand;
            _currentWeapon = gun;
            _currentWeapon.InitWeapon(_weaponHand);

            _inputProvider = inputProvider;
            _inputProvider.OnShootDone += UseCurrentWeapon;
        }

        public void UseCurrentWeapon()
        {
            _currentWeapon.Fire();
        }
    }
}
