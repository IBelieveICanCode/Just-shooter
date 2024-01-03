using UnityEngine;
using TestShooter.Player;

namespace TestShooter
{
    public class TheWorldInfoProvider : Singleton<TheWorldInfoProvider>
    {
        private IHealthDatable _playerHealthData;
        private IEnergyDatable _playerEnergyData;

        public void RegisterPlayerHealth(IHealthDatable healthData)
        {
            _playerHealthData = healthData;
        }

        public IHealthDatable GetPlayerHealthData()
        {
            if (_playerHealthData == null)
            {
                Debug.LogError("Player health is not registered");
            }

            return _playerHealthData;
        }

        public void RegisterPlayerEnergy(IEnergyDatable energyData)
        {
            _playerEnergyData = energyData;
        }

        public IEnergyDatable GetPlayerEnergyData()
        {
            if (_playerEnergyData == null)
            {
                Debug.LogError("Player energy is not registered");
            }

            return _playerEnergyData;
        }
    }
}
