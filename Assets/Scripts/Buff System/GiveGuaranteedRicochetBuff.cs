using System.Collections;
using System.Collections.Generic;
using TestShooter.Player;
using TestShooter.Shooting.Bullets;
using UnityEngine;

namespace TestShooter.Buffs
{
    public class GiveGuaranteedRicochetBuff : IBuffable
    {
        private PlayerStatsMediator _playerStatsMediator;
        private IBulletBehaviourDatable _bulletProbabilitiesConfig;

        public BuffTypes BuffType => BuffTypes.GuaranteeRichoshet;
        private float HealthLimitForBuff => TheWorldInfoProvider.Instance.GetPlayerHealthData().MaxHealth * HpPercentageThershold;

        private const float HpPercentageThershold = 0.2f;

        public GiveGuaranteedRicochetBuff(PlayerStatsMediator playerStatsMediator, IBulletBehaviourDatable bulletProbabilitiesConfig)
        {
            Dispose();
            _playerStatsMediator = playerStatsMediator;
            _bulletProbabilitiesConfig = bulletProbabilitiesConfig;

            if (_playerStatsMediator != null)
            {
                _playerStatsMediator.OnHealthChanged += CheckHealthForBuff;
            }
            else
            {
                Debug.LogError("No mediator created. Create one before giving it to buffs");
            }
        }

        private void CheckHealthForBuff(float healthAmount)
        {
            if (healthAmount <= HealthLimitForBuff)
            {
                Buff();
                return;
            }

            ResetBuff();
        }

        public void Buff()
        {
            _bulletProbabilitiesConfig.GuaranteåBullet(BulletTypes.Ricochet);
        }

        public void ResetBuff()
        {
            _bulletProbabilitiesConfig.Restore();
        }

        public void Dispose()
        {
            if (_playerStatsMediator == null)
            {
                return;
            }

            _playerStatsMediator.OnHealthChanged -= CheckHealthForBuff;
            _playerStatsMediator?.Dispose();
        }
    }
}
