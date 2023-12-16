using StateStuff;
using System.Collections;
using System.Collections.Generic;
using TestShooter.Player;
using TestShooter.Shooting;
using UnityEngine;
using UnityEngine.AI;

namespace TestShooter.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAdmin : MonoBehaviour, IEnemyable
    {
        [SerializeField] private Transform _playerTransform;

        private IMovable _movementLogic;
        private IDamageable _damageableLogic;
        private ICanUseWeaponable _attackLogic;

        private StateMachine<EnemyAdmin> _stateMachine;

        public Transform PlayerTransform => _playerTransform; //{ get; private set; }
        public Transform Transform => this.transform;
        public NavMeshAgent Agent { get; private set; }
        public float DistanceToPlayer{get; private set;}

        private void Awake()
        {
           _damageableLogic = this.gameObject.AddComponent(typeof(StandartDamageRecevierLogic)) as StandartDamageRecevierLogic;
            Agent = GetComponent<NavMeshAgent>();
            DistanceToPlayer = 2f;
        }

        private void Start()
        {
            _stateMachine = new StateMachine<EnemyAdmin>(this);
            _stateMachine.ChangeState(new FindThePlayerState(DistanceToPlayer));
        }

        public void Init(Transform playerTransform)//, IMovable movementLogic, ICanAttackable attackingLogic)
        {
            //_movementLogic = movementLogic;
            //_attackLogic = attackingLogic;
        }

        private void Update()
        {
            _stateMachine.Update();
        }
    }
}
