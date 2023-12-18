using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TestShooter.Enemy
{
    public class EnemyFlyingMovement : IMovable
    {
        private NavMeshAgent _agent;
        private float _maxHeight;

        public EnemyFlyingMovement(NavMeshAgent agent, float maxHeight)
        {
            _agent = agent;
            _maxHeight = maxHeight;
        }

        public void Move(Vector3 finalPosition)
        {
            _agent.SetDestination(finalPosition);

            Vector3 nextPosition = _agent.nextPosition;
            nextPosition.y = _maxHeight;
            _agent.transform.position = Vector3.MoveTowards(_agent.transform.position, nextPosition, _agent.speed * Time.deltaTime);
        }
    }
}
