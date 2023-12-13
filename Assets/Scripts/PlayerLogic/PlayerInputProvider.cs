using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TestShooter.InputSystem;
using UnityEngine;

namespace TestShooter.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputProvider : MonoBehaviour, IInputable
    {
        private PlayerActions _inputActions;

        public event Action<Vector2> OnMovementDone;
        public event Action<Vector2> OnRotationDone;
        public event Action OnShootDone;

        private void Start()
        {
            _inputActions = new PlayerActions();
            _inputActions.PlayerDefaultInput.Enable();

            _inputActions.PlayerDefaultInput.Shoot.performed += Shoot;
        }

        private void Update()
        {
            Vector2 movementAxis = GetMovementAxis();
            OnMovementDone?.Invoke(movementAxis);

            Vector2 rotationAxis = GetRotationAxis();
            OnRotationDone?.Invoke(rotationAxis);
        }

        private void Shoot(InputAction.CallbackContext context)
        {
            OnShootDone?.Invoke();
        }

        private Vector2 GetMovementAxis()
        {
            return _inputActions.PlayerDefaultInput.Movement.ReadValue<Vector2>();
        }

        private Vector2 GetRotationAxis()
        {
            return _inputActions.PlayerDefaultInput.Rotation.ReadValue<Vector2>();
        }
    }
}
