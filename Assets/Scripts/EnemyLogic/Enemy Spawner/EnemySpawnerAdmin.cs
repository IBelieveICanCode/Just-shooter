using Events;
using ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using TestShooter.Player;
using TestShooter.Timers;
using UnityEngine;

namespace TestShooter.Enemy
{
    public class EnemySpawnerAdmin : MonoBehaviour
    {
        [SerializeField] private EnemyWeightConfig _enemyWeights;
        [SerializeField] private EnemySpawnerConfig _enemySpawnerConfig;

        [SerializeField] private GameObject _flyingEnemyPrefab;
        [SerializeField] private GameObject _shootingEnemyPrefab;

        private Dictionary<Type, GameObject> _enemyPrefabs;
        private PoolType<EnemyAbstract> _enemyPool;

        private Transform _playerTransform;
        private IntervalTimer _enemySpawnTimer;
        private EnemySpawnPositionFinder _spawnPositionFinder;
        private EnemyTypeGetter _enemyGetter;
        private EnemyHordeProvider _enemyHorde;

        private int _activeEnemiesAmount => _enemyHorde.ActiveEnemies;

        public void InitEssentials()
        {
            EventManager.GetEvent<AnnouncePlayerPositionEvent>().StartListening(OnObtainPlayerPosition);
            EventManager.GetEvent<EnemyDeadEvent>().StartListening(OnEnemyIsDead);
            EventManager.GetEvent<KillAllEnemiesEvent>().StartListening(OnKillAllEnemies);
            EventManager.GetEvent<GameIsPausedEvent>().StartListening(OnPause);

            _enemyHorde = new EnemyHordeProvider();
            SetEnemyPool();
        }

        public void InitializeEnemySpawner()
        {
            if (_enemySpawnerConfig == null)
            {
                Debug.LogError("Enemy config is null. Assign it via inspector");
                return;
            }

            OnKillAllEnemies();
            _spawnPositionFinder = new EnemySpawnPositionFinder();
            _enemyGetter = new EnemyTypeGetter(_enemyWeights);
            _enemySpawnTimer = new IntervalTimer(
                    _enemySpawnerConfig.StartingSpawnEnemyDuration, 
                    SpawnEnemy, 
                    _enemySpawnerConfig.SpawnDurationDecrease,
                    _enemySpawnerConfig.MinSpawnDuration);
        }

        private void SetEnemyPool()
        {
            _enemyPrefabs = new Dictionary<Type, GameObject>
            {
                { typeof(FlyingEnemy), _flyingEnemyPrefab },
                { typeof(ShooterEnemy), _shootingEnemyPrefab }
            };

            MultiplePrefabFactory<EnemyAbstract> factory = new MultiplePrefabFactory<EnemyAbstract>(_enemyPrefabs, "Enemy");

            Dictionary<Type, int> enemyTypeCounts = new Dictionary<Type, int>
            {
                { typeof(FlyingEnemy), 25 },
                { typeof(ShooterEnemy), 5 }
            };

            _enemyPool = new PoolType<EnemyAbstract>(factory, enemyTypeCounts, this.transform);
        }

        private void SpawnEnemy()
        {
            if (!IsCanSpawnAdditionalEnemy())
            {
                //Debug.Log($"Can't spawn additional enemy. Already max amount: {_activeEnemiesAmount}");
                return;
            }

            EnemyAbstract enemy = _enemyPool.Allocate(_enemyGetter.GetEnemyType());
            enemy.transform.position = _spawnPositionFinder.CalculateSpawnPoint(this.transform);
            enemy.SetPlayerPosition(_playerTransform);

            _enemyHorde.ActivateEnemy(enemy);
        }

        private void OnObtainPlayerPosition(IPlayerDetectable playerDetect)
        {
            _playerTransform = playerDetect.Transform;
        }

        private void OnEnemyIsDead(IPoolable enemy)
        {
            _enemyHorde.DeactivateEnemy(enemy);
        }

        private void OnKillAllEnemies()
        {
            _enemyHorde.RemoveAllEnemies();
        }

        private bool IsCanSpawnAdditionalEnemy()
        {
            if (_activeEnemiesAmount >= _enemySpawnerConfig.MaxAmountEnemiesAllowed)
            {
                return false;
            }

            return true;
        }

        private void OnPause(bool pause)
        {
            if (pause)
            {
                _enemySpawnTimer.Pause();
                return;
            }

            _enemySpawnTimer.Resume();

        }
    }
}
