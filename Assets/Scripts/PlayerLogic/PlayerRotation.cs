using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Player
{
    public class PlayerRotation : IRotatable
    {

        private Transform _ownerTransform;
        private IInputable _inputProvider;
        private UnityEngine.AI.NavMeshAgent _agent;
        public void Rotate(Vector3 input)
        {
            //Plane plane = new Plane(Vector3.up, _ownerTransform.position);
            //float distance;

            //Ray ray = CameraMainProvider.GetMainCamera().ScreenPointToRay(input);

            //if (plane.Raycast(ray, out distance))
            //{
            //    Vector3 position = ray.GetPoint(distance);
            //    _ownerTransform.LookAt(position);
            //}

            if (input.sqrMagnitude < 0.01)
                return; // No significant input, no rotation

            // Calculate the target angle based on the input
            float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg - 90f;

            // Set the player's rotation directly to the target angle
            _ownerTransform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }
    }
}
