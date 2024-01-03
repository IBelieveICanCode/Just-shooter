using UnityEngine;

namespace TestShooter.Shooting.Bullets
{
    public interface IBulletSettingable
    {
        public BulletTypes GetBullet();
        public void Restore();
    }
}
