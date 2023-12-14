using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Shooting.Bullets
{
    [SerializeField]
    public class BulletsChance
    {
        [Range(0, 100)]
        [SerializeField] private float _defaultProbability;
        [Range(0, 100)]
        [SerializeField] private float _currentProbability;
        public float Probability => _currentProbability;
        [field: SerializeField] public BulletTypes Type { get; }

        public void RemoveAllChances()
        {
            _currentProbability = 0;
        }

        public void GuaranteeProbability()
        {
            _currentProbability = 100;
        }

        public void RevertToDefault()
        {
            _currentProbability = _defaultProbability;
        }
    }
}
