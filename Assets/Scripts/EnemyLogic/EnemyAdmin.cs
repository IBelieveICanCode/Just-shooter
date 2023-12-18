using StateStuff;
using System.Collections;
using System.Collections.Generic;
using TestShooter.Player;
using TestShooter.Shooting;
using TestShooter.Shooting.Bullets;
using UnityEngine;
using UnityEngine.AI;

namespace TestShooter.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAdmin : MonoBehaviour, IEnemyable
    {
        [SerializeField] private float _distanceToPlayer = 0.5f;
        [SerializeField] private Transform _weaponHolster;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private DefaultGun _gun;

        private StateMachine<EnemyAdmin> _stateMachine;

        public Transform PlayerTransform => _playerTransform; //{ get; private set; }
        public Transform Transform => this.transform;
        public DefaultGun Gun => _gun;

        public float ThresholdForNavMeshStopping = 0.2f;
        public float MaxHeight = 1f;
        public float TimeOfFlyingUp = 2f;
        public float DurationBeforeAttack = 0.5f;
        public float LengthOfDiveAttack = 1f;
        public float DiveDuration = 1f;

        public NavMeshAgent Agent { get; private set; }
        public StateMachine<EnemyAdmin> StateMachine => _stateMachine;


        private void Awake()
        {
            IDamageable damageableLogic = this.gameObject.AddComponent(typeof(StandartDamageRecevierLogic)) as StandartDamageRecevierLogic;
            Agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _gun = Instantiate(_gun);
            Gun.InitWeapon(_weaponHolster, new EnemyDefaultBulletSetting());

            _stateMachine = new StateMachine<EnemyAdmin>(this);
            _stateMachine.ChangeState(new AscendInAirState());
        }

        private void Update()
        {
            _stateMachine.Update();
        }
    }
}
