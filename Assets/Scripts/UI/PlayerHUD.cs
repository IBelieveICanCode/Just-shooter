using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestShooter.Player
{
    public class PlayerHUD : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Slider _energySlider;

        private PlayerStatsMediator _statsMediator;
        private TheWorldInfoProvider TheWorld => TheWorldInfoProvider.Instance;

        public void InitPlayerStats(PlayerStatsMediator statsMediator)
        {
            _healthSlider.maxValue = TheWorld.GetPlayerHealthData().MaxHealth;
            _energySlider.maxValue = TheWorld.GetPlayerEnergyData().MaxEnergy;

            Dispose();
            _statsMediator = statsMediator;

            if (_statsMediator != null)
            {
                _statsMediator.OnHealthChanged += UpdateHealthUI;
                _statsMediator.OnEnergyChanged += UpdateEnergyUI;
            }
            else
            {
                Debug.LogError("No mediator created. Create one before initing hud");
            }
        }

        private void UpdateHealthUI(float health)
        {
            _healthSlider.value = health;
        }

        private void UpdateEnergyUI(float energy)
        {
            _energySlider.value = energy;
        }

        private void Dispose()
        {
            if (_statsMediator == null)
            {
                return;
            }

            _statsMediator.OnHealthChanged -= UpdateHealthUI;
            _statsMediator.OnEnergyChanged -= UpdateEnergyUI;
            _statsMediator?.Dispose();
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}
