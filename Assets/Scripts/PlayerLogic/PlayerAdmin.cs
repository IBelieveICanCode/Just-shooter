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
using UnityEngine.SceneManagement;

namespace TestShooter.Player
{
    [RequireComponent(typeof(IInputable), typeof(NavMeshAgent))]
    public class PlayerAdmin : MonoBehaviour, IPlayerDetectable
    {
        private IDamageable _healthDamageableLogic;

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

            new PlayerMovement(this.transform, this._agent, this._inputProvider);
            new PlayerRotationPlatformMediator().GetRotationLogic(this.transform, this._inputProvider);

            new PlayerResourceObtainer(healthOperator, energyUseOperator);
            new PlayerCaster(energyUseOperator, _inputProvider);

            healthOperator.UpdateHealth();
            energyUseOperator.UpdateEnergy();
        }

        public void InitShottingLogic(IWeaponable startingGun, IBulletBehaviourDatable bulletSettings)
        {
            bulletSettings.Restore();
            new PlayerShooter(_weaponHand, startingGun, this._inputProvider, bulletSettings);
        }

        public void InitStats(float speed)
        {
            _agent.speed = speed;
        }

        private void OnMyDeath()
        {
            _healthDamageableLogic.OnDeath -= OnMyDeath;
            EventManager.GetEvent<GameOverEvent>().TriggerEvent();
        }
    }
}
