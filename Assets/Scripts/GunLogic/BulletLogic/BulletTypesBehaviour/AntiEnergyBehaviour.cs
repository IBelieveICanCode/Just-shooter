using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Shooting.Bullets
{
    public class AntiEnergyBehaviour : IBulletBehavior
    {
        public BulletTypes Type => BulletTypes.AntiEnergy;

        public void ExecuteBehavior(Bullet bullet, Collider collision, IDamageable damageable)
        {
            EventManager.GetEvent<PassResourceToPlayerEvent>().TriggerEvent(ResourceType.Energy, -bullet.Damage); //TODO replace with damageable interface and logic for energy 
            bullet.Die();
        }
    }
}
