using Events;
using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using TestShooter.Enemy;
using TestShooter.Player;
using TestShooter.Shooting;
using TestShooter.Shooting.Bullets;
using UnityEngine;

namespace TestShooter
{
    public class MainAdmin : MonoBehaviour
    {
        [SerializeField] PlayerHUDAdmin _playerHud;

        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private EnemySpawner _enemySpawner;

        [SerializeField] private HealthConfig _playerHealthConfig;
        [SerializeField] private EnergyConfig _playerEnergyConfig;
        [SerializeField] private GameObject _playerGunPrefab; //TODO arsenal or smth
        [SerializeField] private BulletProbabilitiesConfig _bulletsSettingsConfig;

        private IFactory<PlayerAdmin> _playerFactory;
        private IFactory<DefaultGun> _defaultGunFactory;

        private PlayerStatsMediator _mediator;

        private void Awake()
        {
            EventManager.InitEssentials();
            _enemySpawner.InitEssentials();

            TheWorldInfoProvider.Instance.RegisterPlayerHealth(_playerHealthConfig);
            TheWorldInfoProvider.Instance.RegisterPlayerEnergy(_playerEnergyConfig);

            _playerFactory = new PrefabFactory<PlayerAdmin>(_playerPrefab);
            _defaultGunFactory = new PrefabFactory<DefaultGun>(_playerGunPrefab);
        }

        private void Start()
        {
            CreatePlayer();
            //_enemySpawner.InitializeEnemySpawner();
        }

        private void CreatePlayer()
        {
            PlayerAdmin player = _playerFactory.Create();
            var playerHealthOperator = new StandartHealthOperator(_playerHealthConfig);
            var playerEnergyOperator = new StandartEnergyOperator(_playerEnergyConfig);

            CreateMediator(playerHealthOperator, playerEnergyOperator);
            _playerHud.InitPlayerStats(_mediator);

            player.Init(playerHealthOperator, playerEnergyOperator);
            player.InitShottingLogic(_defaultGunFactory.Create(), _bulletsSettingsConfig);

            EventManager.GetEvent<AnnouncePlayerPositionEvent>().TriggerEvent(player);
        }

        private void CreateMediator(StandartHealthOperator health, StandartEnergyOperator energy)
        {
            _mediator = new PlayerStatsMediator();
            _mediator.SubscribeToHealthOperator(health);
            _mediator.SubscribeToEnergyOperator(energy);
        }

        private void OnDestroy()
        {
            _mediator?.Cleanup();
        }
    }
}
