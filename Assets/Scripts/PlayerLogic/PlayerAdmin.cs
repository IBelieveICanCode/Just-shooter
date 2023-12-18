using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TestShooter.InputSystem;
using TestShooter.Shooting;
using TestShooter.Shooting.Bullets;
using UnityEngine;
using UnityEngine.AI;

namespace TestShooter.Player
{
    [RequireComponent(typeof(IInputable), typeof(NavMeshAgent))]
    public class PlayerAdmin : MonoBehaviour, IPlayerDetectable
    {
        private IInputable _inputProvider;
        private NavMeshAgent _agent;

        [SerializeField] private BulletProbabilitiesConfig _bulletProbabilitiesConfig;
        [SerializeField] private Transform _weaponHand;
        [SerializeField] private DefaultGun _gun;

        public Transform Transform => this.transform;

        private void Awake()
        {
            _inputProvider = GetComponent<PlayerInputProvider>();
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            var movementLogic = new PlayerMovement(this.transform, this._agent, this._inputProvider);
            
            _bulletProbabilitiesConfig.Restore(); ;
            var shootLogic = new PlayerShootingLogic(_weaponHand, Instantiate(_gun), this._inputProvider, _bulletProbabilitiesConfig);
        }
    }
}
