using System.Collections;
using System.Collections.Generic;
using TestShooter.Shooting.Bullets;
using UnityEngine;

namespace TestShooter.Shooting
{
    [CreateAssetMenu(fileName = "Gun Settings", menuName = "Configs/New Gun Settings")]
    public class GunSettingsConfig : ScriptableObject
    {
        [field: SerializeField] public float Cooldown {get; private set;}
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float BulletSpeed { get; private set; }
        [field: SerializeField] public Bullet BulletPrefab { get; private set; }
    }
}
