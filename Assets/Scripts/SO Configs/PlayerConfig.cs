using System.Collections;
using System.Collections.Generic;
using TestShooter.Shooting.Bullets;
using UnityEngine;

namespace TestShooter.Player
{
    [CreateAssetMenu(fileName = "Player Config", menuName = "Configs/New Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public GameObject PlayerPrefab { get; private set; }
        [field: SerializeField] public HealthConfig PlayerHealthConfig { get; private set; }
        [field: SerializeField] public EnergyConfig PlayerEnergyConfig { get; private set; }
        [field: SerializeField] public GameObject PlayerGunPrefab { get; private set; }
        [field: SerializeField] public BulletProbabilitiesConfig BulletsSettingsConfig { get; private set; }

    }
}
