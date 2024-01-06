using System.Collections;
using System.Collections.Generic;
using TestShooter.GameCamera;
using TestShooter.InputSystem;
using UnityEngine;

namespace TestShooter.Player
{
    public class PlayerMobileRotation : IRotatable
    {
        private Transform _ownerTransform;
        private IInputable _inputProvider;

        public PlayerMobileRotation(Transform ownerTransform, IInputable inputProvider)
        {
            _ownerTransform = ownerTransform;
            _inputProvider = inputProvider;

            _inputProvider.OnRotationDone += Rotate;
        }

        public void Rotate(Vector3 input)
        {
            if (input.sqrMagnitude < 0.01)
                return;

            var cameraAngle = Utilities.WrapAngle(CameraMainProvider.GetMainCamera().transform.rotation.eulerAngles.y);
            float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cameraAngle;
            _ownerTransform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }
    }
}
