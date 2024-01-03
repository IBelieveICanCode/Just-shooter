using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TestShooter.InputSystem;
using UnityEngine;
using Events;

namespace TestShooter.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputProvider : MonoBehaviour, IInputable
    {
        private bool _isPaused = false;
        private PlayerActions _inputActions;

        public event Action<Vector3> OnMovementDone;
        public event Action<Vector3> OnRotationDone;
        public event Action OnShootDone;
        public event Action OnEnergyUseDone;

        private void Start()
        {
            _inputActions = new PlayerActions();
            _inputActions.PlayerDefaultInput.Enable();

            _inputActions.PlayerDefaultInput.Shoot.performed += Shoot;
            _inputActions.PlayerDefaultInput.Pause.performed += TogglePause;
            _inputActions.PlayerDefaultInput.EnergyUse.performed += UseEnergy;
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

        private Vector3 GetMovementAxis()
        {
            Vector2 movementAxis = _inputActions.PlayerDefaultInput.Movement.ReadValue<Vector2>();
            return movementAxis;
        }

        private Vector3 GetRotationAxis()
        {
            return _inputActions.PlayerDefaultInput.Rotation.ReadValue<Vector2>();
        }

        private void TogglePause(InputAction.CallbackContext context)
        {
            _isPaused = !_isPaused;
            EventManager.GetEvent<GameIsPausedEvent>().TriggerEvent(_isPaused);
            SetGameplayActionsEnabled(!_isPaused);
        }

        private void UseEnergy(InputAction.CallbackContext context)
        {
            OnEnergyUseDone?.Invoke();
        }

        private void SetGameplayActionsEnabled(bool enabled)
        {
            if (enabled)
            {
                _inputActions.PlayerDefaultInput.Movement.Enable();
                _inputActions.PlayerDefaultInput.Rotation.Enable();
                _inputActions.PlayerDefaultInput.Shoot.Enable();
                _inputActions.PlayerDefaultInput.EnergyUse.Enable();
            }
            else
            {
                _inputActions.PlayerDefaultInput.Movement.Disable();
                _inputActions.PlayerDefaultInput.Rotation.Disable();
                _inputActions.PlayerDefaultInput.Shoot.Disable();
                _inputActions.PlayerDefaultInput.EnergyUse.Disable();
            }
        }
    }
}
