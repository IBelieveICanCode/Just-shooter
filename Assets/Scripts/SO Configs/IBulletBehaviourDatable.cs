using UnityEngine;

namespace TestShooter.Shooting.Bullets
{
    public interface IBulletBehaviourDatable
    {
        public void GuaranteåBullet(BulletTypes Type);
        public BulletTypes GetBullet();
        public void Restore();
    }
}
