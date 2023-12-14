using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Shooting.Bullets
{
    public class PhasingBullet : IBulletBehavior
    {
        public BulletTypes Type => BulletTypes.Phasing;

        public void ExecuteBehavior(Bullet bullet, Collider collision)
        {
        }
    }
}
