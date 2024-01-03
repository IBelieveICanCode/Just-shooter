using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Enemy
{
    public class EnemyTypeGetter
    {
        public int TotalEnemiesSpawned { get; private set; }

        public Type GetEnemyType()
        {
            Type enemyType = TotalEnemiesSpawned % 5 == 4 ? typeof(ShootingEnemy) : typeof(FlyingEnemy); //TODO remake to more elaborate logic
            TotalEnemiesSpawned++;
            return enemyType;
        }
    }
}
