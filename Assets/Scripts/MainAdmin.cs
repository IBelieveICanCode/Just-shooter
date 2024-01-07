using Events;
using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using TestShooter.Buffs;
using TestShooter.Enemy;
using TestShooter.Player;
using TestShooter.Shooting;
using TestShooter.Shooting.Bullets;
using TestShooter.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TestShooter
{
    public class MainAdmin : MonoBehaviour
    {
        [SerializeField] private PlayerHUD _playerHud;
        [SerializeField] private PlayerConfig _playerConfig;

        [SerializeField] private EnemyWeightConfig _enemyWeights;
        [SerializeField] private EnemySpawnerAdmin _enemySpawner;

        private IFactory<PlayerAdmin> _playerFactory;
        private IFactory<DefaultGun> _defaultGunFactory;

        private PlayerStatsMediator _mediator;

        private IWeaponable _playerDefaultGun; //TODO Arsenal system
        private IBuffable _ricochetBuff; //TODO system of buffs here 
        private PlayerAdmin _player;

        private void Awake()
        {
            EventManager.InitEssentials();
            _enemySpawner.InitEssentials();

            TheWorldInfoProvider.Instance.RegisterPlayerHealth(_playerConfig.PlayerHealthConfig);
            TheWorldInfoProvider.Instance.RegisterPlayerEnergy(_playerConfig.PlayerEnergyConfig);

            _playerFactory = new PrefabFactory<PlayerAdmin>(_playerConfig.PlayerPrefab);
            _defaultGunFactory = new PrefabFactory<DefaultGun>(_playerConfig.PlayerGunPrefab);
        }

        private void Start()
        {
            EventManager.GetEvent<StartGameEvent>().StartListening(StartNewGame);
            StartTheGame();
        }

        private void StartTheGame()
        {
            CreatePlayer();
            CreateBuffsForPlayer();
            _enemySpawner.InitializeEnemySpawner(_enemyWeights);

            EventManager.GetEvent<GameIsPausedEvent>().TriggerEvent(false);
        }

        private void CreatePlayer()
        {
            _player = _player ?? _playerFactory.Create();
            var playerHealthOperator = new StandartHealthOperator(_playerConfig.PlayerHealthConfig);
            var playerEnergyOperator = new StandartEnergyOperator(_playerConfig.PlayerEnergyConfig);

            CreatePlayerMediator(playerHealthOperator, playerEnergyOperator);

            _playerHud.InitPlayerStats(_mediator);

            _player.InitStats(_playerConfig.Speed);
            _player.Init(playerHealthOperator, playerEnergyOperator);

            _playerDefaultGun = _playerDefaultGun ?? _defaultGunFactory.Create();
            _player.InitShottingLogic(_playerDefaultGun, _playerConfig.BulletsSettingsConfig);

            EventManager.GetEvent<AnnouncePlayerPositionEvent>().TriggerEvent(_player);
        }

        private void CreatePlayerMediator(StandartHealthOperator health, StandartEnergyOperator energy)
        {
            _mediator?.Dispose();
            _mediator = _mediator ?? new PlayerStatsMediator();
            _mediator.SubscribeToHealthOperator(health);
            _mediator.SubscribeToEnergyOperator(energy);
        }

        private void CreateBuffsForPlayer()
        {
            _ricochetBuff = new GiveGuaranteedRicochetBuff(_mediator, _playerConfig.BulletsSettingsConfig);
        }

        private void StartNewGame()
        {
            EventManager.GetEvent<StartGameEvent>().StopListening(StartNewGame); //TODO Optimization task - don't load the whole scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnDestroy()
        {
            _mediator?.Dispose();
            _ricochetBuff.Dispose();
        }
    }
}
