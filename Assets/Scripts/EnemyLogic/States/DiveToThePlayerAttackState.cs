using StateStuff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Enemy
{
    public class DiveToThePlayerAttackState : State<EnemyAdmin>
    {
        EnemyDiveAttack _enemyDiveAttack;

        public override void EnterState(EnemyAdmin owner)
        {
            owner.Agent.updatePosition = false;
            Debug.Log($"Current state is {GetType().Name}");

            var nextState = new FlyingToThePlayerState();
            _enemyDiveAttack = new EnemyDiveAttack(
                owner.Agent,
                owner.PlayerTransform,
                owner.LengthOfDiveAttack,
                owner.DiveDuration,
                () => owner.StateMachine.ChangeState(new FlyingToThePlayerState()));

            _enemyDiveAttack.DiveAttack();
        }

        public override void ExitState(EnemyAdmin owner)
        {
        }

        public override void UpdateState(EnemyAdmin owner)
        {
            if (_enemyDiveAttack != null)
            {
                _enemyDiveAttack.Rotate();
            }
        }
    }
}
