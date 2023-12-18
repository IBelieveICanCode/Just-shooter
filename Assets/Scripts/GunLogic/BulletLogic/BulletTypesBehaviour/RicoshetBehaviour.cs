using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Shooting.Bullets
{
    public class RicoshetBehaviour : IBulletBehavior
    {
        private List<IEnemyable> _alreadyHitEnemies = new List<IEnemyable>();
        private float _radius = 20;
        private int _amountOfRicochets = 1;

        public BulletTypes Type => BulletTypes.Ricochet;

        public void ExecuteBehavior(Bullet bullet, Collider firstEnemy)
        {
            if (_amountOfRicochets <= 0)
            {
                Debug.Log("No ricoshets behaviour remain");
                bullet.Die();
                return;
            }

            _alreadyHitEnemies.Add(firstEnemy.gameObject.GetComponent<IEnemyable>());
            GameObject enemyToRicochet = FindNearestEnemy(bullet.transform, _radius);

            if (enemyToRicochet == null)
            {
                bullet.Die();
                return;
            }

            _amountOfRicochets--;
            Vector3 direction = enemyToRicochet.transform.position - bullet.transform.position;
            bullet.Init(2, 2, direction);
        }

        private GameObject FindNearestEnemy(Transform thisBullet, float radius)
        {
            Collider[] hitColliders = Physics.OverlapSphere(thisBullet.transform.position, radius);
            GameObject nearestEnemy = null;
            float closestDistance = float.MaxValue;

            foreach (Collider hitCollider in hitColliders)
            {
                IEnemyable foundEnemyComponent = hitCollider.gameObject.GetComponent<IEnemyable>();

                if (!IsEnemyValid(foundEnemyComponent))
                {
                    continue;
                }

                float distance = Vector3.Distance(thisBullet.transform.position, hitCollider.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestEnemy = hitCollider.gameObject;
                }
            }

            return nearestEnemy;
        }

        private bool IsEnemyValid(IEnemyable enemy)
        {
            if (enemy == null)
            {
                Debug.Log("No valid enemy");
                return false;
            }

            if (_alreadyHitEnemies.Contains(enemy))
            {
                Debug.Log("The list is already containing this enemy");
                return false;
            }

            return true;
        }
    }
}
