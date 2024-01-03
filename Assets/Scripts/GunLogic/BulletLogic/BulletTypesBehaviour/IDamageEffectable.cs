using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Shooting
{
    public interface IDamageEffectable
    {
        void ApplyEffect(IDamageable target);
    }
}
