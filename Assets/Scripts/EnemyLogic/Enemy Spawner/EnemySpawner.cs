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
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float _startingSpawnEnemyDuration = 10;
        [SerializeField] private float _spawnDurationDecrease = 2f;
        [SerializeField] private float _minSpawnDuration = 6f;

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
        private const int MaxAmountEnemiesAllowed = 30;

        public void InitEssentials()
        {
            EventManager.GetEvent<AnnouncePlayerPositionEvent>().StartListening(OnObtainPlayerPosition);
            EventManager.GetEvent<EnemyDeadEvent>().StartListening(OnEnemyIsDead);
            EventManager.GetEvent<KillAllEnemiesEvent>().StartListening(OnKillAllEnemies);

            _enemyHorde = new EnemyHordeProvider();
            SetEnemyPool();
        }

        public void InitializeEnemySpawner()
        {
            _spawnPositionFinder = new EnemySpawnPositionFinder();
            _enemyGetter = new EnemyTypeGetter();
            _enemySpawnTimer = new IntervalTimer(_startingSpawnEnemyDuration, SpawnEnemy, _spawnDurationDecrease, _minSpawnDuration);
        }

        private void SetEnemyPool()
        {
            _enemyPrefabs = new Dictionary<Type, GameObject>
            {
                { typeof(FlyingEnemy), _flyingEnemyPrefab },
                { typeof(ShootingEnemy), _shootingEnemyPrefab }
            };

            MultiplePrefabFactory<EnemyAbstract> factory = new MultiplePrefabFactory<EnemyAbstract>(_enemyPrefabs, "Enemy");

            Dictionary<Type, int> enemyTypeCounts = new Dictionary<Type, int>
            {
                { typeof(FlyingEnemy), 25 },
                { typeof(ShootingEnemy), 5 }
            };

            _enemyPool = new PoolType<EnemyAbstract>(factory, enemyTypeCounts, this.transform);
        }

        private void SpawnEnemy()
        {
            if (!IsCanSpawnAdditionalEnemy())
            {
                Debug.Log($"Can't spawn additional enemy. Already max amount: {_activeEnemiesAmount}");
                return;
            }

            EnemyAbstract enemy = _enemyPool.Allocate(_enemyGetter.GetEnemyType());
            enemy.transform.position = _spawnPositionFinder.CalculateSpawnPoint(this.transform);
            enemy.SetPlayerPosition(_playerTransform);

            _enemyHorde.ActivateEnemy(enemy);
            Debug.Log($"Enemy spawned. Active amount: {_activeEnemiesAmount}. Total spawned: {_enemyGetter.TotalEnemiesSpawned}");
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
            if (_activeEnemiesAmount >= MaxAmountEnemiesAllowed)
            {
                return false;
            }

            return true;
        }
    }
}
