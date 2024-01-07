using Events;
using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Enemy
{
    public class EnemyHordeProvider 
    {
        private List<IPoolable> _activeEnemies;
        public int ActiveEnemies => _activeEnemies.Count;

        public EnemyHordeProvider()
        {
            _activeEnemies = new List<IPoolable>();
        }

        public void ActivateEnemy(IPoolable enemy)
        {
            _activeEnemies.Add(enemy);
            enemy.Restore();
        }

        public void DeactivateEnemy(IPoolable enemy)
        {
            _activeEnemies.Remove(enemy);
            enemy.Reset();
        }

        public void RemoveAllEnemies()
        {
            var enemiesToReset = new List<IPoolable>(_activeEnemies);

            foreach (var enemy in enemiesToReset)
            {
                EventManager.GetEvent<EnemyDeadEvent>().TriggerEvent(enemy);
            }
        }
    }
}
