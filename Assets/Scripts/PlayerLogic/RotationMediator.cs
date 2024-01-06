using System.Collections;
using System.Collections.Generic;
using TestShooter.InputSystem;
using UnityEngine;

namespace TestShooter.Player
{
    public class RotationPlatformMediator
    {
        public IRotatable GetRotationLogic(Transform ownerTransform, IInputable inputProvider)
        {
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                return new PlayerMobileRotation(ownerTransform, inputProvider);
            }
            else
            {
                return new PlayerMouseRotation(ownerTransform, inputProvider);
            }

        }

    }
}
