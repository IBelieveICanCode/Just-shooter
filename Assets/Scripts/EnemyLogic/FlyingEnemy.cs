using StateStuff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Enemy
{
    public class FlyingEnemy : EnemyAbstract, ITouchable
    {
        [SerializeField] private float _touchDamage = 15f;
        [SerializeField] private float _maxHeightOfFlying = 1f;
        [SerializeField] private float _timeOfFlyingUp = 2f;
        [SerializeField] private float _durationBeforeAttack = 0.5f;
        [SerializeField] private float _lengthOfDiveAttack = 1f;
        [SerializeField] private float _diveDuration = 1f;


        public float MaxHeightOfFlying => _maxHeightOfFlying;
        public float TimeOfFlyingUp => _timeOfFlyingUp;
        public float DurationBeforeAttack => _durationBeforeAttack;
        public float LengthOfDiveAttack => _lengthOfDiveAttack;
        public float DiveDuration => _diveDuration;

        private StateMachine<FlyingEnemy> _stateMachine;
        public StateMachine<FlyingEnemy> StateMachine => _stateMachine;

        protected override void InitStateMachine()
        {
            _stateMachine = new StateMachine<FlyingEnemy>(this);
            _stateMachine.ChangeState(new AscendInAirState());
        }

        protected override void UpdateStateMachine()
        {
            _stateMachine?.Update();
        }

        public void OnTriggerEnter(Collider collision)
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            if (damageable == null)
            {
                ThisDamageable.Die();
                return;
            }

            damageable.ReceiveDamage(_touchDamage);
            ThisDamageable.Die();
        }
    }
}
