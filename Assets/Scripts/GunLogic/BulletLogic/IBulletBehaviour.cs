using TestShooter.Shooting;
using UnityEngine;

namespace TestShooter.Shooting.Bullets
{
    public interface IBulletBehavior
    {
        BulletTypes Type { get; }
        void ExecuteBehavior(Bullet bullet, Collider collision);
    }
}
