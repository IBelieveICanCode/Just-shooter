using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Enemy
{
    [CreateAssetMenu(fileName = "Enemy Weight Config", menuName = "Configs/New Enemy Weight Config")]
    public class EnemyWeightConfig: ScriptableObject
    {
        [field: SerializeField] public EnemySpawnData[] Enemies { get; private set; }

        [System.Serializable]
        public class EnemySpawnData
        {
            public EnemyTypes Type;
            public int SpawnWeight;
        }

    }
}
