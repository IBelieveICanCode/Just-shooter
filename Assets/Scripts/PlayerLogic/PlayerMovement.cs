using System.Collections;
using System.Collections.Generic;
using TestShooter.InputSystem;
using UnityEngine;

namespace TestShooter.Player
{
    public class PlayerMovement : IMovable
    {
        private Transform _ownerTransform;
        private IInputable _inputProvider;

        public PlayerMovement(Transform ownerTransform, IInputable inputProvider)
        {
            _ownerTransform = ownerTransform;
            _inputProvider = inputProvider;

            _inputProvider.OnMovementDone += Move;
            _inputProvider.OnRotationDone += Rotate;

        }

        public void Move(Vector2 movementAxis)
        {
            Vector3 xzVector = new Vector3(movementAxis.x, 0, movementAxis.y);
            Vector3 targetVector = (Quaternion.Euler(0, CameraProvider.GetMainCamera().transform.eulerAngles.y, 0) * xzVector).normalized;
            var targetPosition = _ownerTransform.position + targetVector * Time.deltaTime;
            _ownerTransform.position = targetPosition;
        }

        public void Rotate(Vector2 input)
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
