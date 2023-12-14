using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Shooting
{
    public interface IWeaponable
    {
        void InitWeapon(Transform holster);
        void Fire();
    }
}
