using System;
using System.Collections;
using System.Collections.Generic;

namespace TestShooter.Shooting.Bullets
{
    public class BulletRandomizer
    {
        private readonly List<BulletsChance> BulletsChances;
        private readonly Random Random;

        // Constructor with seed
        public BulletRandomizer(List<BulletsChance> bulletsChances, int seed)
        {
            BulletsChances = bulletsChances;
            Random = new Random(seed);
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
                    return bulletChance.Type;

                randomPoint -= bulletChance.Probability;
            }

            throw new InvalidOperationException("Unable to select bullet type: probabilities are not set correctly.");
        }
    }
}
