using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.InputSystem
{
    public interface IInputable 
    {
        public event Action<Vector2> OnMovementDone;
        public event Action<Vector2> OnRotationDone;
        public event Action OnShootDone;
    }
}
