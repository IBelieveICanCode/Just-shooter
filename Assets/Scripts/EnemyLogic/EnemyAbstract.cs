using Events;
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
        [SerializeField] private HealthConfig _healthConfig;
        [SerializeField] private float _stoppingDistance = 0.5f;
        [SerializeField] private Transform _weaponHolster;
        [SerializeField] private DefaultGun _gun;

        public IDamageable EnemyDamageableLogic { get; private set; }
        public Transform PlayerTransform { get; private set; }
        public Transform Transform => this.transform;
        public DefaultGun Gun => _gun;

        public NavMeshAgent Agent { get; private set; }

        #region Init block
        private void Awake()
        {
            if (EnemyDamageableLogic == null)
            {
                EnemyDamageableLogic = this.gameObject.AddComponent(typeof(StandartDamageRecevier)) as StandartDamageRecevier;
                IHealthOperatorable healthOperator = new StandartHealthOperator(_healthConfig);
                EnemyDamageableLogic.InitHealth(healthOperator);
            }

            Agent = GetComponent<NavMeshAgent>();
            Agent.stoppingDistance = _stoppingDistance;

            EnemyDamageableLogic.OnDeath += OnMyDeath;
        }

        private void Start()
        {
            _gun = Instantiate(_gun);
            Gun.InitWeapon(_weaponHolster, new EnemyDefaultBulletSetting());
        }

        public void PlaceUnderParent(Transform parent)
        {
            this.transform.parent = parent;
        }

        public void SetPlayerPosition(Transform player)
        {
            PlayerTransform = player;
        }

        protected abstract void InitStateMachine();
        protected abstract void UpdateStateMachine();
        #endregion

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
            InitStateMachine();
        }

        private void OnMyDeath()
        {
            EnemyDamageableLogic.OnDeath -= OnMyDeath;
            EventManager.GetEvent<EnemyDeadEvent>().TriggerEvent(this);
            Debug.Log($"I am dead: {this.name}");
        }
    }
}
