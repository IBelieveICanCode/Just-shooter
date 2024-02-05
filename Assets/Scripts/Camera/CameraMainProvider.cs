using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.GameCamera
{
    public class CameraMainProvider : MonoBehaviour
    {
        private static Camera _mainCamera;

        void Awake()
        {
            _mainCamera = Camera.main;
        }

        public static Camera GetMainCamera()
        {
            if (_mainCamera == null)
            {
                _mainCamera = Camera.main;
            }

            return _mainCamera;
        }
    }
}