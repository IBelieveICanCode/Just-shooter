using System.Collections;
using System.Collections.Generic;
using TestShooter.GameCamera;
using TestShooter.InputSystem;
using UnityEngine;
using UnityEngine.AI;

namespace TestShooter.Player
{
    public class PlayerMovement : IMovable //IRotatable
    {
        private Transform _ownerTransform;
        private IInputable _inputProvider;
        private NavMeshAgent _agent;

        public PlayerMovement(Transform ownerTransform, NavMeshAgent agent, IInputable inputProvider)
        {
            _ownerTransform = ownerTransform;
            _inputProvider = inputProvider;
            _agent = agent;

            _inputProvider.OnMovementDone += Move;
        }

        public void Move(Vector3 movementAxis)
        {
            Vector3 xzVector = new Vector3(movementAxis.x, 0, movementAxis.y);
            Vector3 worldDirection = Quaternion.Euler(0, CameraMainProvider.GetMainCamera().transform.eulerAngles.y, 0) * xzVector;

            _agent.Move(worldDirection * Time.deltaTime * _agent.speed);
            _agent.SetDestination(_ownerTransform.position + worldDirection);
        }      
    }
}
