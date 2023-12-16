using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Shooting.Bullets
{
    public class BulletBehaviourFactory : IFactory<IBulletBehavior>
    {
        private BulletProbabilitiesConfig _bulletProbabilitiesConfig;

        public BulletBehaviourFactory(BulletProbabilitiesConfig bulletProbabilitiesConfig)
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
                default:
                {
                    return new DefaultBullet();
                }
            }
        }


    }
}
