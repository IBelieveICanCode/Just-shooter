using System.Collections;
using System.Collections.Generic;
using TestShooter.InputSystem;
using UnityEngine;
using UnityEngine.AI;

namespace TestShooter.Player
{
    public class PlayerMovement : IMovable
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
            _inputProvider.OnRotationDone += Rotate;

        }

        public void Move(Vector3 movementAxis)
        {
            //Vector3 xzVector = new Vector3(movementAxis.x, 0, movementAxis.y);
            //Vector3 targetVector = (Quaternion.Euler(0, CameraProvider.GetMainCamera().transform.eulerAngles.y, 0) * xzVector).normalized;
            //var targetPosition = _ownerTransform.position + targetVector * Time.deltaTime;
            //_ownerTransform.position = targetPosition;

            //Vector3 xzVector = new Vector3(movementAxis.x, 0, movementAxis.y);
            //Vector3 worldDirection = Quaternion.Euler(0, CameraProvider.GetMainCamera().transform.eulerAngles.y, 0) * xzVector;
            //Vector3 targetPosition = _ownerTransform.position + worldDirection;
            //_agent.SetDestination(targetPosition);

            Vector3 xzVector = new Vector3(movementAxis.x, 0, movementAxis.y);
            Vector3 worldDirection = Quaternion.Euler(0, CameraProvider.GetMainCamera().transform.eulerAngles.y, 0) * xzVector;

            _agent.Move(worldDirection * Time.deltaTime * _agent.speed);
            _agent.SetDestination(_ownerTransform.position + worldDirection);
        }

        public void Rotate(Vector3 input)
        {
            Plane plane = new Plane(Vector3.up, _ownerTransform.position);
            float distance;

            Ray ray = CameraProvider.GetMainCamera().ScreenPointToRay(input);

            if (plane.Raycast(ray, out distance))
            {
                Vector3 position = ray.GetPoint(distance);
                _ownerTransform.LookAt(position);
            }
        }
    }
}
