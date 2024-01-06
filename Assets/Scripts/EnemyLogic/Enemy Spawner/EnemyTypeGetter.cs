using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Enemy
{
    public class EnemyTypeGetter
    {
        private EnemyWeightConfig _weightConfig;
        public int TotalEnemiesSpawned { get; private set; }

        public EnemyTypeGetter(EnemyWeightConfig config)
        {
            _weightConfig = config;
        }

        public Type GetEnemyType()
        {
            if (_weightConfig == null)
            {
                Debug.LogError("Weight config is not assigned");
                return null;
            }

            int totalWeight = 0;
            foreach (var enemy in _weightConfig.Enemies)
            {
                totalWeight += enemy.SpawnWeight;
            }

            int randomValue = UnityEngine.Random.Range(0, totalWeight);
            int cumulativeWeight = 0;

            foreach (var enemy in _weightConfig.Enemies)
            {
                cumulativeWeight += enemy.SpawnWeight;

                if (randomValue < cumulativeWeight)
                {
                    return ConvertToType(enemy.Type);
                }
            }

            throw new InvalidOperationException("Unable to select an enemy type.");
        }

        private Type ConvertToType(EnemyTypes type)
        {
            switch (type)
            {
                case EnemyTypes.Flying:
                {
                    return typeof(FlyingEnemy);
                }
                case EnemyTypes.Shooter:
                {
                    return typeof(ShooterEnemy);
                }
            }

            throw new InvalidOperationException($"Can't return type of {type}. Make sure you added logic for it"); 
        }
    }
}
