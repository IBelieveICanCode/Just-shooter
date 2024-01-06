using StateStuff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Enemy
{
    public class ShooterEnemy : EnemyAbstract
    {
        private StateMachine<ShooterEnemy> _stateMachine;
        public StateMachine<ShooterEnemy> StateMachine => _stateMachine;

        protected override void InitStateMachine()
        {
            _stateMachine = new StateMachine<ShooterEnemy>(this);
            _stateMachine.ChangeState(new FindAndShootThePlayerState());
        }

        protected override void UpdateStateMachine()
        {
            _stateMachine?.Update();
        }
    }
}
