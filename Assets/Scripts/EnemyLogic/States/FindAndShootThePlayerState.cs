using StateStuff;
using System.Collections;
using System.Collections.Generic;
using TestShooter.Shooting;
using UnityEngine;
using UnityEngine.AI;

namespace TestShooter.Enemy
{
    public class FindAndShootThePlayerState : State<EnemyAdmin>
    {
        private IMovable _moveLogic;
        private IRotatable _rotateLogic;
        private IWeaponable _currentWeapon;

        public override void EnterState(EnemyAdmin owner)
        {
            owner.Agent.isStopped = false;
            _moveLogic = new EnemyGroundMovement(owner.Agent);
            _rotateLogic = new EnemyBasicRotation(owner.Transform);
            _currentWeapon = owner.Gun;
        }

        public override void ExitState(EnemyAdmin owner)
        {
            owner.Agent.isStopped = true;
        }

        public override void UpdateState(EnemyAdmin owner)
        {
            if (owner.PlayerTransform == null)
            {
                return;
            }

            _moveLogic.Move(owner.PlayerTransform.position);
            _rotateLogic.Rotate(owner.PlayerTransform.position);
            _currentWeapon.Fire();

            if (owner.Agent.IsTooCloseTo(owner.PlayerTransform.position, owner.ThresholdForNavMeshStopping))
            {
                owner.StateMachine.ChangeState(new StandAndShootThePlayerState());
            }
        }
    }
}
