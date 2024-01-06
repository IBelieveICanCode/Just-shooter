using System.Collections;
using System.Collections.Generic;
using TestShooter.GameCamera;
using TestShooter.InputSystem;
using UnityEngine;

namespace TestShooter.Player
{
    public class PlayerMouseRotation : IRotatable
    {
        private Transform _ownerTransform;
        private IInputable _inputProvider;

        public PlayerMouseRotation(Transform ownerTransform, IInputable inputProvider)
        {
            _ownerTransform = ownerTransform;
            _inputProvider = inputProvider;

            _inputProvider.OnRotationDone += Rotate;
        }

        public void Rotate(Vector3 input)
        {
            Plane plane = new Plane(Vector3.up, _ownerTransform.position);
            float distance;

            Ray ray = CameraMainProvider.GetMainCamera().ScreenPointToRay(input);

            if (plane.Raycast(ray, out distance))
            {
                Vector3 position = ray.GetPoint(distance);
                _ownerTransform.LookAt(position);
            }
        }
    }
}
