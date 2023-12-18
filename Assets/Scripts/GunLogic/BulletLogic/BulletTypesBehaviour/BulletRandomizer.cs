using System;
using System.Collections;
using System.Collections.Generic;

namespace TestShooter.Shooting.Bullets
{
    public class BulletRandomizer
    {
        private readonly List<BulletsChance> BulletsChances;
        private readonly Random Random;

        public BulletRandomizer(List<BulletsChance> bulletsChances)
        {
            BulletsChances = bulletsChances;
            Random = new Random();
        }

        public BulletTypes GetRandomBulletType()
        {
            float totalProbability = 0;

            foreach (var bulletChance in BulletsChances)
            {
                totalProbability += bulletChance.Probability;
            }

            float randomPoint = (float)(Random.NextDouble() * totalProbability);
            foreach (var bulletChance in BulletsChances)
            {
                if (randomPoint < bulletChance.Probability)
                {
                    UnityEngine.Debug.Log($"Randomizer returned type of bullet: {bulletChance.Type} based on probability {bulletChance.Probability}");
                    return bulletChance.Type;
                }

                randomPoint -= bulletChance.Probability;
            }

            throw new InvalidOperationException("Unable to select bullet type: probabilities are not set correctly.");
        }
    }
}
