using System.Collections;
using System.Collections.Generic;
using TestShooter.InputSystem;
using TestShooter.Shooting;
using UnityEngine;

namespace TestShooter.Player
{
    public class PlayerShootingLogic : ICanShootable
    {
        private Transform _weaponHand;

        private IInputable _inputProvider;
        private IGunable _currentGun;
        public IGunable CurrentGun => _currentGun;

        public PlayerShootingLogic(Transform weaponHand, IGunable gun, IInputable inputProvider)
        {
            _weaponHand = weaponHand;
            _currentGun = gun;
            _inputProvider = inputProvider;

            InitWeapon();
            _inputProvider.OnShootDone += UseCurrentWeapon;
        }

        private void InitWeapon()
        {
            _currentGun.InitGun(_weaponHand);
        }

        public void UseCurrentWeapon()
        {
            _currentGun.Fire();
        }
    }
}
