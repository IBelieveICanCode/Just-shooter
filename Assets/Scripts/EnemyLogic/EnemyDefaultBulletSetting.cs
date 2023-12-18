using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Shooting.Bullets
{
    public class EnemyDefaultBulletSetting : IBulletSettingable
    {
        public BulletTypes GetBullet()
        {
            return BulletTypes.Default;
        }

        public void Restore()
        {
        }

    }
}
