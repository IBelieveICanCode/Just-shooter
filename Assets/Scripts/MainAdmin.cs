using Events;
using System.Collections;
using System.Collections.Generic;
using TestShooter.Enemy;
using TestShooter.Player;
using UnityEngine;

namespace TestShooter
{
    public class MainAdmin : MonoBehaviour
    {
        [SerializeField] private PlayerAdmin _player;
        [SerializeField] private EnemySpawner _enemySpawner;

        private void Awake()
        {
            EventManager.Init();
            _enemySpawner.Init();
        }

        private void Start()
        {
            EventManager.GetEvent<AnnouncePlayerPosition>().TriggerEvent(_player.transform);
            _enemySpawner.InitializeEnemySpawner();
        }
    }
}
