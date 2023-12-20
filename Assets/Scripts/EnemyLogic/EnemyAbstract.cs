using ObjectPool;
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
    public abstract class EnemyAbstract : MonoBehaviour, IEnemyable, IPoolable
    {
        [SerializeField] private float _stoppingDistance = 0.5f;
        [SerializeField] private Transform _weaponHolster;
        [SerializeField] private DefaultGun _gun;

        protected IDamageable ThisDamageable;

        public Transform PlayerTransform; //{ get; private set; }
        public Transform Transform => this.transform;
        public DefaultGun Gun => _gun;

        public NavMeshAgent Agent { get; private set; }

        private void Awake()
        {
            ThisDamageable = this.gameObject.AddComponent(typeof(StandartDamageRecevierLogic)) as StandartDamageRecevierLogic;
            Agent = GetComponent<NavMeshAgent>();
            Agent.stoppingDistance = _stoppingDistance;
        }

        private void Start()
        {
            _gun = Instantiate(_gun);
            Gun.InitWeapon(_weaponHolster, new EnemyDefaultBulletSetting());
            InitStateMachine();
        }


        private void Update()
        {
            UpdateStateMachine();
        }

        public void Reset()
        {
            gameObject.SetActive(false);
        }

        public void Restore()
        {
            gameObject.SetActive(true);
        }

        public void PlaceUnderParent(Transform parent)
        {
            this.transform.parent = parent;
        }

        protected abstract void InitStateMachine();
        protected abstract void UpdateStateMachine();
    }
}
