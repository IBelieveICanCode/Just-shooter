using StateStuff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Enemy
{
    public class FlyingToThePlayerState : State<FlyingEnemy>
    {
        IMovable _moveLogic;
        IRotatable _rotateLogic;

        public override void EnterState(FlyingEnemy owner)
        {
            owner.Agent.updatePosition = false;
            _moveLogic = new EnemyFlyingMovement(owner.Agent, owner.MaxHeightOfFlying);
            _rotateLogic = new EnemyBasicRotation(owner.Agent.transform);
        }

        public override void ExitState(FlyingEnemy owner)
        {
        }

        public override void UpdateState(FlyingEnemy owner)
        {
            if (owner.PlayerTransform == null)
            {
                return;
            }

            _moveLogic.Move(owner.PlayerTransform.position);
            _rotateLogic.Rotate(owner.PlayerTransform.position);

            if (owner.Agent.IsTooCloseTo(owner.PlayerTransform.position))
            {
                var nextState = new DiveToThePlayerAttackState();
                owner.StateMachine.ChangeState(new StopState<FlyingEnemy>(owner.StateMachine, nextState, owner.DurationBeforeAttack));
            }
        }
    }
}
