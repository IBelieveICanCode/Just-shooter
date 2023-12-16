using System.Collections;
using System.Collections.Generic;
using TestShooter.Shooting;
using UnityEngine;

namespace TestShooter.Enemy
{
    public class EnemyShooting : ICanUseWeaponable
    {
        private Transform _weaponHand;
        private IWeaponable _currentWeapon;

        public EnemyShooting(IWeaponable weapon, Transform weaponHand)
        {
            _currentWeapon = weapon;
            _weaponHand = weaponHand;

            _currentWeapon.InitWeapon(_weaponHand);
        }

        public void UseCurrentWeapon()
        {
            _currentWeapon.Fire();
        }
    }
}
