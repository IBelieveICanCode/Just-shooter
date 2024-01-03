using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Player
{
    public class StandartEnergyOperator: IEnergyOperatorable
    {
        private float _energy;
        private float _maxEnergy;

        public event Action<float> OnEnergyChanged;

        public float Energy => _energy;
        public bool IsMaxEnergy => _energy >= _maxEnergy;

        public StandartEnergyOperator(IEnergyDatable energyData)
        {
            _maxEnergy = energyData.MaxEnergy;
            _energy = Mathf.Clamp(energyData.StartingEnergy, 0, _maxEnergy);
            OnEnergyChanged?.Invoke(_energy);
        }

        public void AddEnergy(float amount)
        {
            _energy += amount;
            _energy = Mathf.Clamp(_energy, 0, _maxEnergy);
            OnEnergyChanged?.Invoke(_energy);
        }

        public void SubstractEnergy(float amount)
        {
            _energy -= amount;
            _energy = Mathf.Clamp(_energy, 0, _maxEnergy);
            OnEnergyChanged?.Invoke(_energy);
        }

        public void UpdateEnergy()
        {
            OnEnergyChanged?.Invoke(_energy);
        }
    }
}
