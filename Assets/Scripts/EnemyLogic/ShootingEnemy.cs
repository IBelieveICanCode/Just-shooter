using StateStuff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Enemy
{
    public class ShootingEnemy : EnemyAbstract
    {
        private StateMachine<ShootingEnemy> _stateMachine;
        public StateMachine<ShootingEnemy> StateMachine => _stateMachine;

        protected override void InitStateMachine()
        {
            //_stateMachine = new StateMachine<ShootingEnemy>(this);
            //_stateMachine.ChangeState(new FindAndShootThePlayerState());
        }

        protected override void UpdateStateMachine()
        {
            _stateMachine?.Update();
        }
    }
}
