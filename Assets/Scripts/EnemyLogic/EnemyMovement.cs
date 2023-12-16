using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TestShooter.Enemy
{
    public class EnemyMovement : IMovable
    {
        private NavMeshAgent _agent;
        private Transform _ownerTransform;

        public EnemyMovement(Transform ownerTransform, NavMeshAgent agent)
        {
            _agent = agent;
            _ownerTransform = ownerTransform;
        }

        public void Move(Vector3 finalPosition)
        {
            _agent.SetDestination(finalPosition);
        }

        public void Rotate(Vector3 finalPosition)
        {
            Vector3 direction = finalPosition - _ownerTransform.position;
            direction.y = 0;
            Quaternion newRotation = Quaternion.LookRotation(direction);
            _ownerTransform.rotation = newRotation;
        }
    }
}
