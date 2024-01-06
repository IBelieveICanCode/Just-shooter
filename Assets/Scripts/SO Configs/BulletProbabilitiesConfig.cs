using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestShooter.Shooting.Bullets
{
    [CreateAssetMenu(fileName = "Bullet Probabilities", menuName = "Configs/Probabilities Config")]
    public class BulletProbabilitiesConfig : ScriptableObject, IBulletBehaviourDatable
    {
        [SerializeField] private List<BulletsChance> _bulletChances;

        public void GuaranteåBullet(BulletTypes Type)
        {
            var foundBulletType = _bulletChances.FirstOrDefault(chance => chance.Type == Type);

            if (foundBulletType == null)
            {
                Debug.Log($"There is no bullet with type {Type} in config");
                return;
            }

            foundBulletType.GuaranteeProbability();
            _bulletChances.Where(chance => chance.Type != Type).ToList().ForEach(chance => chance.RemoveAllChances());
        }

        public void Restore()
        {
            _bulletChances.ForEach(chance => chance.SetToDefault());
        }

        public BulletTypes GetBullet()
        {
            BulletRandomizer randomizer = new BulletRandomizer(_bulletChances);
            return randomizer.GetRandomBulletType();
        }
    }
}
