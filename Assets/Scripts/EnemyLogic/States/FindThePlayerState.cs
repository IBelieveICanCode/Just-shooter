using StateStuff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TestShooter.Enemy
{
    public class FindThePlayerState : State<EnemyAdmin>
    {
        private IMovable _moveLogic;
        private float _distanceToThePlayer;

        public FindThePlayerState(float distanceToThePlayer)
        {
            _distanceToThePlayer = distanceToThePlayer;
        }

        public override void EnterState(EnemyAdmin owner)
        {
            _moveLogic = new EnemyMovement(owner.Transform, owner.Agent);
        }

        public override void ExitState(EnemyAdmin owner)
        {
            
        }

        public override void UpdateState(EnemyAdmin owner)
        {
            if (_moveLogic == null)
            {
                return;
            }

            _moveLogic.Move(owner.PlayerTransform.position);
            _moveLogic.Rotate(owner.PlayerTransform.position);

            if (Vector3.Distance(owner.Transform.position, owner.PlayerTransform.position) < _distanceToThePlayer)
            {
                owner.Agent.isStopped = true;
                Debug.Log("Reached");
            }
        }
    }
}
