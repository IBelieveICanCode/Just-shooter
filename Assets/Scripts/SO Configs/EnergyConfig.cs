using System.Collections;
using System.Collections.Generic;
using TestShooter;
using UnityEngine;

[CreateAssetMenu(fileName = "New Energy", menuName = "Configs/Energy Settings")]
public class EnergyConfig : ScriptableObject, IEnergyDatable
{
    [field: SerializeField] public float StartingEnergy { get; private set; }
    [field: SerializeField] public float MaxEnergy { get; private set; }
}
