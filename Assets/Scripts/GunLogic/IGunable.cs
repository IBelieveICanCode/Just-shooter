using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Shooting
{
    public interface IGunable
    {
        void InitGun(Transform holster);
        void Fire();
    }
}
