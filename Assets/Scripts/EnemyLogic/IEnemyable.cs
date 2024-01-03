using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Enemy
{
    public interface IEnemyable
    {
        IDamageable EnemyDamageableLogic { get; }
        Transform Transform { get; }
    }
}
