using StateStuff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Enemy
{
    public class FlyingEnemy : EnemyAbstract, ITouchable
    {
        [SerializeField] private FlyingEnemyConfig _flyingSettingConfig;
        private StateMachine<FlyingEnemy> _stateMachine;

        public float MaxHeightOfFlying => _flyingSettingConfig.MaxHeightOfFlying;
        public float TimeOfFlyingUp => _flyingSettingConfig.TimeOfFlyingUp;
        public float DurationBeforeAttack => _flyingSettingConfig.DurationBeforeAttack;
        public float LengthOfDiveAttack => _flyingSettingConfig.LengthOfDiveAttack;
        public float DiveDuration => _flyingSettingConfig.DiveDuration;

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
            if (collision.gameObject.layer != LayerMask.NameToLayer(Utilities.Playerlayer))
            {
                return;
            }

            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            if (damageable == null)
            {
                return;
            }

            damageable.ReceiveDamage(_flyingSettingConfig.TouchDamage);
            EnemyDamageableLogic.Die();
        }
    }
}
