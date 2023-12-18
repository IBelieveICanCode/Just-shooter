using StateStuff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Enemy
{
    public class FlyingToThePlayerState : State<EnemyAdmin>
    {
        IMovable _moveLogic;
        IRotatable _rotateLogic;

        public override void EnterState(EnemyAdmin owner)
        {
            owner.Agent.updatePosition = false;
            _moveLogic = new EnemyFlyingMovement(owner.Agent, owner.MaxHeight);
            Debug.Log($"Current state is {GetType().Name}");
            _rotateLogic = new EnemyBasicRotation(owner.Agent.transform);
        }

        public override void ExitState(EnemyAdmin owner)
        {
        }

        public override void UpdateState(EnemyAdmin owner)
        {
            if (owner.PlayerTransform == null)
            {
                return;
            }

            _moveLogic.Move(owner.PlayerTransform.position);
            _rotateLogic.Rotate(owner.PlayerTransform.position);

            if (owner.Agent.IsTooCloseTo(owner.PlayerTransform.position, owner.ThresholdForNavMeshStopping))
            {
                var nextState = new DiveToThePlayerAttackState();
                owner.StateMachine.ChangeState(new StopState(nextState,owner.DurationBeforeAttack));
            }
        }
    }
}
