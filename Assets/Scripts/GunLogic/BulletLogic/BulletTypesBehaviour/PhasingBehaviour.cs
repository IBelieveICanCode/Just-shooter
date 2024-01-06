using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Shooting.Bullets
{
    public class PhasingBehaviour : IBulletBehavior
    {
        public BulletTypes Type => BulletTypes.Phasing;

        public void ExecuteBehavior(Bullet bullet, Collider collision, IDamageable damageable)
        {
            damageable.ReceiveDamage(bullet.Damage);
        }
    }
}
