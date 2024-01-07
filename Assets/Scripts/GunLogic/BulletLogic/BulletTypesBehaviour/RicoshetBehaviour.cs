using System.Collections;
using System.Collections.Generic;
using TestShooter.Enemy;
using UnityEngine;

namespace TestShooter.Shooting.Bullets
{
    public class RicoshetBehaviour : IBulletBehavior
    {
        private List<IEnemyable> _alreadyHitEnemies = new List<IEnemyable>();
        private float _radius = 10;
        private int _amountOfRicochets;
        private int _maxAmountOfRicochets = 1;

        public BulletTypes Type => BulletTypes.Ricochet;

        public RicoshetBehaviour()
        {
            _amountOfRicochets = _maxAmountOfRicochets;
        }

        public void ExecuteBehavior(Bullet bullet, Collider collider, IDamageable damageable)
        {
            damageable.ReceiveDamage(bullet.Damage);

            IEnemyable enemy = collider.gameObject.GetComponent<IEnemyable>();

            if (_amountOfRicochets <= 0)
            {
                CheckEnemyDieFromBullet(enemy.EnemyDamageableLogic, bullet.Damage);
                bullet.Die();
                return;
            }

            _alreadyHitEnemies.Add(enemy);
            IEnemyable enemyToRicochet = FindNearestEnemy(bullet.transform, _radius);

            if (enemyToRicochet == null)
            {
                bullet.Die();
                return;
            }

            CheckEnemyDieFromBullet(enemy.EnemyDamageableLogic, bullet.Damage);

            _amountOfRicochets--;
            Vector3 direction = enemyToRicochet.Transform.position - bullet.transform.position;
            bullet.Init(bullet.Damage, bullet.Speed, direction);
        }

        private IEnemyable FindNearestEnemy(Transform thisBullet, float radius)
        {
            IEnemyable[] hitColliders = Utilities.GetEnemiesInRadius(thisBullet.transform.position, radius);
            IEnemyable nearestEnemy = null;
            float closestDistance = float.MaxValue;

            foreach (IEnemyable enemy in hitColliders)
            {
                if (!IsEnemyValid(enemy))
                {
                    continue;
                }

                float distance = Vector3.Distance(thisBullet.transform.position, enemy.Transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestEnemy = enemy;
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
