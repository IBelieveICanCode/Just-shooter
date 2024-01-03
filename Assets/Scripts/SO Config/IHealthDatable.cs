using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter
{
    public interface IHealthDatable
    {
        float StartingHealth { get; }
        float MaxHealth { get; }
    }
}
