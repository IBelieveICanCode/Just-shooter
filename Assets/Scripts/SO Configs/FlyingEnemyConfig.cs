using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Enemy
{
    [CreateAssetMenu(fileName = "Flying Enemy Config", menuName = "Configs/New Flying Enemy Config")]
    public class FlyingEnemyConfig : ScriptableObject
    {
        [field: SerializeField] public float TouchDamage { get; private set; }
        [field: SerializeField] public float MaxHeightOfFlying { get; private set; }
        [field: SerializeField] public float TimeOfFlyingUp { get; private set; }
        [field: SerializeField] public float DurationBeforeAttack { get; private set; }
        [field: SerializeField] public float LengthOfDiveAttack { get; private set; }
        [field: SerializeField] public float DiveDuration { get; private set; }
    }
}
