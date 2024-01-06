using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Shooting.Bullets
{
    public class EnemyDefaultBulletSetting : IBulletBehaviourDatable
    {
        public BulletTypes GetBullet()
        {
            return BulletTypes.AntiEnergy;
        }

        public void GuaranteåBullet(BulletTypes Type)
        {
        }

        public void Restore()
        {
        }

    }
}
