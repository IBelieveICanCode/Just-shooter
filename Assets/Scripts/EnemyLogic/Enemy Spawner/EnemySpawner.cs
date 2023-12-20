using Events;
using ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
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

        private Transform _playerPosition;
        private IntervalTimer _enemySpawnTimer;
        private EnemySpawnPositionFinder _spawnPositionFinder;

        public void Init()
        {
            EventManager.GetEvent<AnnouncePlayerPosition>().StartListening(ObtainPlayerPosition);

            _enemyPrefabs = new Dictionary<Type, GameObject>
            {
                { typeof(FlyingEnemy), _flyingEnemyPrefab },
                { typeof(ShootingEnemy), _shootingEnemyPrefab }
            };

            MultiplePrefabFactory<EnemyAbstract> factory = new MultiplePrefabFactory<EnemyAbstract>(_enemyPrefabs, "Enemy");

            Dictionary<Type, int> enemyTypeCounts = new Dictionary<Type, int>
            {
                { typeof(FlyingEnemy), 5 },
                { typeof(ShootingEnemy), 5 }
            };

            _enemyPool = new PoolType<EnemyAbstract>(factory, enemyTypeCounts, this.transform);
        }

        public void InitializeEnemySpawner()
        {
            _spawnPositionFinder = new EnemySpawnPositionFinder();
            _enemySpawnTimer = new IntervalTimer(_startingSpawnEnemyDuration, SpawnEnemy, _spawnDurationDecrease, _minSpawnDuration);
        }

        private void SpawnEnemy()
        {
            EnemyAbstract enemy = _enemyPool.Allocate(UnityEngine.Random.value > 0.5 ? typeof(FlyingEnemy) : typeof(ShootingEnemy)); //TODO Refactor and make separate class
            enemy.gameObject.SetActive(true);
            enemy.transform.position = _spawnPositionFinder.CalculateSpawnPoint(this.transform);
            enemy.PlayerTransform = _playerPosition;
        }

        private void ObtainPlayerPosition(Transform playerPosition)
        {
            _playerPosition = playerPosition;
        }
    }
}
