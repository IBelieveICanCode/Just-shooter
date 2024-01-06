using UnityEngine;

namespace TestShooter.Shooting.Bullets
{
    public interface IBulletBehaviourDatable
    {
        public void Guarante�Bullet(BulletTypes Type);
        public BulletTypes GetBullet();
        public void Restore();
    }
}
