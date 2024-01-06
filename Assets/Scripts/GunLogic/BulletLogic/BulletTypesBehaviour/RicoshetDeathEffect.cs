using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Shooting.Bullets
{
    public class RicoshetDeathEffect : IDeathEffectable
    {
        private const float EnergyProbability = 0.7f;
        private const float EnergyToPass = 10f;

        private const float HealthDivider = 2f;

        public void TriggerDeathEffect()
        {
            if (Random.value < EnergyProbability) //TODO separate and probably complex class
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
            var healthToPass = playerMaxHealth / HealthDivider;
            Debug.Log($"Gave health: {healthToPass}");

            EventManager.GetEvent<PassResourceToPlayerEvent>().TriggerEvent(ResourceType.Health, healthToPass);
        }

        private void GivePlayerEnergy()
        {
            Debug.Log($"Gave energy: {EnergyToPass}");
            EventManager.GetEvent<PassResourceToPlayerEvent>().TriggerEvent(ResourceType.Energy, EnergyToPass);
        }
    }
}
