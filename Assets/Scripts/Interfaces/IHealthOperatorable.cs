using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter
{
    public interface IHealthOperatorable
    {
        event Action<float> OnHealthChanged;
        float Health { get; }
        float MaxHealth { get; }
        void AddHealth(float amount);
        void SubstractHealth(float amount);
        void UpdateHealth();
    }
}