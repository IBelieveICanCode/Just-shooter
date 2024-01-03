using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter
{
    public interface IEnergyOperatorable
    {
        event Action<float> OnEnergyChanged;
        bool IsMaxEnergy { get; }
        float Energy { get; }
        void SubstractEnergy(float amount);
        void AddEnergy(float amount);
        void UpdateEnergy();
    }
}
