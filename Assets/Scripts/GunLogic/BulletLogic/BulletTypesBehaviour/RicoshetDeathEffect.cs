using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Shooting.Bullets
{
    public class RicoshetDeathEffect : IDeathEffectable
    {
        private float _energyProbability = 0.7f;
        private float _energyToPass = 10f;

        private float _healthDivider = 2f;

        public void TriggerDeathEffect()
        {
            if (Random.value < _energyProbability) //TODO separate and probably complex class
            {
                GivePlayerEnergy();
            }
            else
            {
                GivePlayerHealth();
            }
        }

        private void GivePlayerHealth()
        {
            var playerMaxHealth = TheWorldInfoProvider.Instance.GetPlayerHealthData().MaxHealth;
            var healthToPass = playerMaxHealth / _healthDivider;
            Debug.Log($"Gave health: {healthToPass}");

            EventManager.GetEvent<PassResourceToPlayerEvent>().TriggerEvent(ResourceType.Health, healthToPass);
        }

        private void GivePlayerEnergy()
        {
            EventManager.GetEvent<PassResourceToPlayerEvent>().TriggerEvent(ResourceType.Energy, _energyToPass);
        }
    }
}
