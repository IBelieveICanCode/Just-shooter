using System.Collections;
using System.Collections.Generic;
using TestShooter.Shooting.Bullets;
using UnityEngine;

namespace TestShooter.Shooting
{
    public interface IWeaponable
    {
        void InitWeapon(Transform holster, IBulletBehaviourDatable bulletSetting);
        void Fire();
    }
}
