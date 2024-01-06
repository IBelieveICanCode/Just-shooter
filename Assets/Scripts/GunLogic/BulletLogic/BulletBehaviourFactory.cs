using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Shooting.Bullets
{
    public class BulletBehaviourFactory : IFactory<IBulletBehavior>
    {
        private IBulletBehaviourDatable _bulletProbabilitiesConfig;

        public BulletBehaviourFactory(IBulletBehaviourDatable bulletProbabilitiesConfig)
        {
            _bulletProbabilitiesConfig = bulletProbabilitiesConfig;
        }

        public IBulletBehavior Create()
        {
            BulletTypes bulletType = _bulletProbabilitiesConfig.GetBullet();

            switch (bulletType)
            {
                case BulletTypes.Phasing:
                {
                    return new PhasingBehaviour();
                }
                case BulletTypes.Ricochet:
                {
                    return new RicoshetBehaviour();
                }
                case BulletTypes.AntiEnergy:
                {
                    return new AntiEnergyBehaviour(); 
                }
                default:
                {
                    return new DefaultBullet();
                }
            }
        }
    }
}
