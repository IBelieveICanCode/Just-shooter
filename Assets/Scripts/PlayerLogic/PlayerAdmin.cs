using Cinemachine;
using Events;
using System;
using System.Collections;
using System.Collections.Generic;
using TestShooter.InputSystem;
using TestShooter.Shooting;
using TestShooter.Shooting.Bullets;
using TestShooter.Spells;
using UnityEngine;
using UnityEngine.AI;

namespace TestShooter.Player
{
    [RequireComponent(typeof(IInputable), typeof(NavMeshAgent))]
    public class PlayerAdmin : MonoBehaviour, IPlayerDetectable
    {
        private IDamageable _healthDamageableLogic;
        private IMovable _movementLogic;
        private IRotatable _rotationLogic;
        private ICanUseWeaponable _shootLogic;
        private ISpellCasterable _spellCastLogic;
        private IResourceObtainable _resourceObtainLogic;

        private IInputable _inputProvider;
        private NavMeshAgent _agent;

        [SerializeField] private Transform _weaponHand;

        public Transform Transform => this.transform;

        private void Awake()
        {
            _inputProvider = GetComponent<PlayerInputProvider>();
            _agent = GetComponent<NavMeshAgent>();
        }

        public void Init(IHealthOperatorable healthOperator, IEnergyOperatorable energyUseOperator)
        {
            if (_healthDamageableLogic == null)
            {
                _healthDamageableLogic = this.gameObject.AddComponent(typeof(StandartDamageRecevier)) as StandartDamageRecevier;
                _healthDamageableLogic.InitHealth(healthOperator);
                _healthDamageableLogic.OnDeath += OnMyDeath;
            }

            _movementLogic = new PlayerMovement(this.transform, this._agent, this._inputProvider);
            _rotationLogic = new RotationPlatformMediator().GetRotationLogic(this.transform, this._inputProvider);

            _resourceObtainLogic = new PlayerResourceObtainer(healthOperator, energyUseOperator);
            _spellCastLogic = new PlayerCaster(energyUseOperator, _inputProvider);

            healthOperator.UpdateHealth();
            energyUseOperator.UpdateEnergy();
        }

        public void InitShottingLogic(IWeaponable startingGun, IBulletBehaviourDatable bulletSettings)
        {
            bulletSettings.Restore();
            _shootLogic = new PlayerShooter(_weaponHand, startingGun, this._inputProvider, bulletSettings);
        }

        private void OnMyDeath()
        {
            _healthDamageableLogic.OnDeath -= OnMyDeath;
            EventManager.GetEvent<PlayerDeadEvent>().TriggerEvent();
        }
    }
}
