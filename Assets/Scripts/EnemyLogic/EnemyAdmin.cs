using System.Collections;
using System.Collections.Generic;
using TestShooter.Player;
using TestShooter.Shooting;
using UnityEngine;

namespace TestShooter.Enemy
{
    public class EnemyAdmin : MonoBehaviour, IEnemyable
    {
        private IMovable _movementLogic;
        private IDamageable _damageableLogic;
        private ICanAttackable _attackLogic;

        public Transform Transform => this.transform;

        private void Awake()
        {
           _damageableLogic = this.gameObject.AddComponent(typeof(PlayerDamageRecevierLogic)) as PlayerDamageRecevierLogic;
        }

        public void Init(IDamageable damageableLogic)//, IMovable movementLogic, ICanAttackable attackingLogic)
        {
            //_movementLogic = movementLogic;
            _damageableLogic = damageableLogic;
            //_attackLogic = attackingLogic;
        }
    }
}
