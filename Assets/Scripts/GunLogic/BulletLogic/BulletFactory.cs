using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Shooting.Bullets
{
    public class BulletFactory : IFactory<IBulletBehavior>
    {
        private List<BulletsChance> _bulletChances;

        public BulletFactory(List<BulletsChance> bulletChances)
        {
            _bulletChances = bulletChances;
        }

        public IBulletBehavior Create()
        {

        }
    }
}
