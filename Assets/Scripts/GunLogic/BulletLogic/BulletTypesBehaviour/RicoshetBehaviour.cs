using System.Collections;
using System.Collections.Generic;
using TestShooter.Enemy;
using UnityEngine;

namespace TestShooter.Shooting.Bullets
{
    public class RicoshetBehaviour : IBulletBehavior
    {
        private List<IEnemyable> _alreadyHitEnemies = new List<IEnemyable>();
        private float _radius = 20;
        private int _amountOfRicochets;
        private int _maxAmountOfRicochets = 1;

        public BulletTypes Type => BulletTypes.Ricochet;

        public RicoshetBehaviour()
        {
            _amountOfRicochets = _maxAmountOfRicochets;
        }

        public void ExecuteBehavior(Bullet bullet, Collider collider)
        {
            IEnemyable enemy = collider.gameObject.GetComponent<IEnemyable>();

            if (_amountOfRicochets <= 0)
            {
                CheckEnemyDieFromBullet(enemy.EnemyDamageableLogic, bullet.Damage);
                bullet.Die();
                return;
            }

            _alreadyHitEnemies.Add(enemy);
            GameObject enemyToRicochet = FindNearestEnemy(bullet.transform, _radius);

            if (enemyToRicochet == null)
            {
                bullet.Die();
                return;
            }

            CheckEnemyDieFromBullet(enemy.EnemyDamageableLogic, bullet.Damage);

            _amountOfRicochets--;
            Vector3 direction = enemyToRicochet.transform.position - bullet.transform.position;
            bullet.Init(bullet.Damage, bullet.Speed, direction);
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
                return false;
            }

            if (_alreadyHitEnemies.Contains(enemy))
            {
                Debug.Log("The list is already containing this enemy");
                return false;
            }

            return true;
        }

        private void CheckEnemyDieFromBullet(IDamageable enemy, float damage)
        {
            if (IfFirstEnemyThatHit())
            {
                return;
            }

            if (enemy.Health <= damage)
            {
                IDeathEffectable deathEffect = new RicoshetDeathEffect();
                deathEffect.TriggerDeathEffect();
            }
        }

        private bool IfFirstEnemyThatHit()
        {
            return _amountOfRicochets == _maxAmountOfRicochets;
        }
    }
}
