using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter
{
    [CreateAssetMenu(fileName = "New Health", menuName = "Configs/Health Settings")]
    public class HealthConfig : ScriptableObject, IHealthDatable
    {
        [field: SerializeField] public float StartingHealth { get; private set; }
        [field: SerializeField] public float MaxHealth { get; private set; }

    }
}