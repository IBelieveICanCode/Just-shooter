using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter
{
    public interface IEnergyDatable
    {
        float StartingEnergy { get; }
        float MaxEnergy { get; }
    }
}
