using Events;
using ObjectPool;
using TestShooter.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TestShooter.UI
{
    public class PlayerHUD : MonoBehaviour
    {
        [SerializeField] private Button _ultaButton;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Slider _energySlider;
        [SerializeField] private TextMeshProUGUI _enemiesKilledText;

        private int _enemiesKilled = 0;
        private PlayerStatsMediator _statsMediator;

        private const string EnemiesKilledFormat = "Enemies killed: {0}";
        private TheWorldInfoProvider TheWorld => TheWorldInfoProvider.Instance;

        private void Start()
        {
            EventManager.GetEvent<GameOverEvent>().StartListening(Dispose);
            EventManager.GetEvent<EnemyDeadEvent>().StartListening(UpdateEnemyKilledCount);
        }

        public void InitPlayerStats(PlayerStatsMediator statsMediator)
        {
            _healthSlider.maxValue = TheWorld.GetPlayerHealthData().MaxHealth;
            _energySlider.maxValue = TheWorld.GetPlayerEnergyData().MaxEnergy;

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

            if (_energySlider.value == _energySlider.maxValue)
            {
                _ultaButton.interactable = true;
                return;
            }

            _ultaButton.interactable = false;
        }

        private void UpdateEnemyKilledCount(IPoolable enemy)
        {
            _enemiesKilled++;
            _enemiesKilledText.text = string.Format(EnemiesKilledFormat, _enemiesKilled);
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
            _statsMediator = null;
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}
