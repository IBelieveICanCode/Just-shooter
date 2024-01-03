using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestShooter.Player
{
    public class PlayerHUDAdmin : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Slider _energySlider;

        private PlayerStatsMediator _statsMediator;

        public void InitPlayerStats(PlayerStatsMediator statsMediator)
        {
            _healthSlider.maxValue = TheWorldInfoProvider.Instance.GetPlayerHealthData().MaxHealth;
            _energySlider.maxValue = TheWorldInfoProvider.Instance.GetPlayerEnergyData().MaxEnergy;

            CleanUpMediator();
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

        private void CleanUpMediator()
        {
            if (_statsMediator == null)
            {
                return;
            }

            _statsMediator.OnHealthChanged -= UpdateHealthUI;
            _statsMediator.OnEnergyChanged -= UpdateEnergyUI;
            _statsMediator?.Cleanup();
        }

        void OnDestroy()
        {
            CleanUpMediator();
        }
    }
}
