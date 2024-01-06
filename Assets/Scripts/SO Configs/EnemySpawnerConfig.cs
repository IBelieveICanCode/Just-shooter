using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Enemy
{
    [CreateAssetMenu(fileName = "Enemy Spawner Settings", menuName = "Configs/New Enemy Spawner Settings")]
    public class EnemySpawnerConfig: ScriptableObject
    {
        [field: SerializeField] public float StartingSpawnEnemyDuration {get; private set;}
        [field: SerializeField] public float SpawnDurationDecrease {get; private set;}
        [field: SerializeField] public float MinSpawnDuration{get; private set;}
        [field: SerializeField] public int MaxAmountEnemiesAllowed {get; private set;}
    }
}
