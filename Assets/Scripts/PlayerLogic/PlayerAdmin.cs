using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TestShooter.InputSystem;
using TestShooter.Shooting;
using UnityEngine;

namespace TestShooter.Player
{
    [RequireComponent(typeof(IInputable))]
    public class PlayerAdmin : MonoBehaviour
    {
        private IInputable _inputProvider;
        private IMovable _movementLogic;
        private IDamageable _damageableLogic;
        private ICanAttackable _shootLogic;

        [SerializeField] private Transform _weaponHand;
        [SerializeField] private DefaultGun _gun;
 
        private void Awake()
        {
            _inputProvider = GetComponent<PlayerInputProvider>();
        }

        private void Start()
        {
            _movementLogic = new PlayerMovement(this.transform, this._inputProvider);
            _damageableLogic = this.gameObject.AddComponent(typeof(PlayerDamageRecevierLogic)) as PlayerDamageRecevierLogic;

            _shootLogic = new PlayerShootingLogic(_weaponHand, Instantiate(_gun), this._inputProvider);
        }
    }
}
