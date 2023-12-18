using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TestShooter.Enemy
{
    public class EnemyDiveAttack
    {
        private Vector3[] _pathPoints;
        private int _currentPathIndex;

        private NavMeshAgent _agent;
        private Transform _ownerTransform;
        private Transform _target;
        private float _beyondTargetDistance;
        private float _diveDuration;

        private Action _onCompleteAction;

        public EnemyDiveAttack(NavMeshAgent agent, Transform target, float beyondTargetDistance, float diveDuration, Action onCompleteAction = null)
        {
            _agent = agent;
            _ownerTransform = agent.transform;
            _target = target;
            _beyondTargetDistance = beyondTargetDistance;
            _diveDuration = diveDuration;
            _onCompleteAction = onCompleteAction;
            _pathPoints = new Vector3[3];
        }
        public void Rotate()
        {
            Vector3 direction = (_pathPoints[_currentPathIndex] - _ownerTransform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _ownerTransform.rotation = Quaternion.Slerp(_ownerTransform.rotation, lookRotation, Time.deltaTime * _diveDuration);
        }

        public void DiveAttack()
        {
            Vector3 diveEndPoint = CalculateDiveEndPoint();
            StartDiveAttack(_target.position, diveEndPoint);
        }

        private void StartDiveAttack(Vector3 midPoint, Vector3 endPoint)
        {
            _currentPathIndex = 0;
    
            _pathPoints[0] = _ownerTransform.position;
            _pathPoints[1] = midPoint;
            _pathPoints[2] = endPoint;

            _ownerTransform.DOPath(_pathPoints, _diveDuration, PathType.CatmullRom)
                .SetLookAt(0.01f)
                .OnWaypointChange(index => _currentPathIndex = index)
                .OnComplete(() =>
                {
                    _onCompleteAction?.Invoke();
                });
        }

        private Vector3 CalculateDiveEndPoint()
        {
            Vector3 directionTowardsPlayer = (_target.position - _ownerTransform.position).normalized;
            Vector3 endPoint = _target.position + directionTowardsPlayer * _beyondTargetDistance;
            endPoint.y = _ownerTransform.position.y;
            return endPoint;
        }
    }
}
