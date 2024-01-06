using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Shooting.Bullets
{
    public class DefaultBullet : IBulletBehavior
    {
        public BulletTypes Type => BulletTypes.Default;

        public void ExecuteBehavior(Bullet bullet, Collider collision, IDamageable damageable)
        {
            damageable.ReceiveDamage(bullet.Damage);
            bullet.Die();
        }
    }
}
