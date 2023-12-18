using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TestShooter.Enemy
{
    public class EnemyGroundMovement : IMovable
    {
        private NavMeshAgent _agent;

        public EnemyGroundMovement(NavMeshAgent agent)
        {
            _agent = agent;
        }

        public void Move(Vector3 finalPosition)
        {
            _agent.SetDestination(finalPosition);
        }        
    }
}
