using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Player
{
    public class PlayerStatsMediator
    {
        private StandartHealthOperator _healthOperator;
        private StandartEnergyOperator _energyOperator;

        public event Action<float> OnHealthChanged;
        public event Action<float> OnEnergyChanged;

        public void SubscribeToHealthOperator(StandartHealthOperator healthOperator)
        {
            UnsubscribeFromHealthOperator();
            _healthOperator = healthOperator;
            healthOperator.OnHealthChanged += HealthChanged;
        }

        public void SubscribeToEnergyOperator(StandartEnergyOperator energyOperator)
        {
            UnsubscribeFromEnergyOperator();
            _energyOperator = energyOperator;
            energyOperator.OnEnergyChanged += EnergyChanged;
        }

        private void HealthChanged(float health)
        {
            OnHealthChanged?.Invoke(health);
        }

        private void EnergyChanged(float energy)
        {
            OnEnergyChanged?.Invoke(energy);
        }

        public void UnsubscribeFromHealthOperator()
        {
            if (_healthOperator != null)
            {
                _healthOperator.OnHealthChanged -= HealthChanged;
                _healthOperator = null;
            }
        }

        public void UnsubscribeFromEnergyOperator()
        {
            if (_energyOperator != null)
            {
                _energyOperator.OnEnergyChanged -= EnergyChanged;
                _energyOperator = null;
            }
        }

        public void Dispose()
        {
            UnsubscribeFromHealthOperator();
            UnsubscribeFromEnergyOperator();
        }
    }
}
