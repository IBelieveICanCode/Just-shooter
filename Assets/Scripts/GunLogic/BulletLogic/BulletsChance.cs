using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Shooting.Bullets
{
    [System.Serializable]
    public class BulletsChance
    {
        [SerializeField] private BulletTypes _type;
        [Range(0, 100)]
        [SerializeField] private float _defaultProbability;
        private float _currentProbability; //TODO custom editor to forbid user from setting more than 100 total 
        public float Probability => _currentProbability;
        public BulletTypes Type => _type;

        public void RemoveAllChances()
        {
            _currentProbability = 0;
        }

        public void GuaranteeProbability()
        {
            _currentProbability = 100;
        }

        public void SetToDefault()
        {
            _currentProbability = _defaultProbability;
        }
    }
}
