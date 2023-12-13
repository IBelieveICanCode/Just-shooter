using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter
{
    public class CameraProvider : MonoBehaviour
    {
        private static Camera mainCamera;

        void Awake()
        {
            mainCamera = Camera.main;
        }

        public static Camera GetMainCamera()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }

            return mainCamera;
        }
    }
}