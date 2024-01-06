using Events;
using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using TestShooter.Buffs;
using TestShooter.Enemy;
using TestShooter.Player;
using TestShooter.Shooting;
using TestShooter.Shooting.Bullets;
using UnityEngine;

namespace TestShooter
{
    public class MainAdmin : MonoBehaviour
    {
        [SerializeField] PlayerHUD _playerHud;
        [SerializeField] private EnemySpawnerAdmin _enemySpawner;
        [SerializeField] private PlayerConfig _playerConfig;

        private IFactory<PlayerAdmin> _playerFactory;
        private IFactory<DefaultGun> _defaultGunFactory;

        private PlayerStatsMediator _mediator;
        private IBuffable _ricochetBuff; //TODO system of buffs here 

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
            CreatePlayer();
            CreateBuffsForPlayer();
            _enemySpawner.InitializeEnemySpawner();
        }

        private void CreatePlayer()
        {
            PlayerAdmin player = _playerFactory.Create();
            var playerHealthOperator = new StandartHealthOperator(_playerConfig.PlayerHealthConfig);
            var playerEnergyOperator = new StandartEnergyOperator(_playerConfig.PlayerEnergyConfig);

            CreatePlayerMediator(playerHealthOperator, playerEnergyOperator);
            _playerHud.InitPlayerStats(_mediator);        

            player.Init(playerHealthOperator, playerEnergyOperator);
            player.InitShottingLogic(_defaultGunFactory.Create(), _playerConfig.BulletsSettingsConfig);

            EventManager.GetEvent<AnnouncePlayerPositionEvent>().TriggerEvent(player);
        }

        private void CreatePlayerMediator(StandartHealthOperator health, StandartEnergyOperator energy)
        {
            _mediator = new PlayerStatsMediator();
            _mediator.SubscribeToHealthOperator(health);
            _mediator.SubscribeToEnergyOperator(energy);
        }

        private void CreateBuffsForPlayer()
        {
            _ricochetBuff = new GiveGuaranteedRicochetBuff(_mediator, _playerConfig.BulletsSettingsConfig);
        }

        private void OnDestroy()
        {
            _mediator?.Dispose();
            _ricochetBuff.Dispose();
        }
    }
}
