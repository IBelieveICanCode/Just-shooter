using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Enemy
{
    [CreateAssetMenu(fileName = "Enemy Loot", menuName = "Configs/New Enemy Loot")]
    public class EnemyLoot : ScriptableObject, ILootable
    {
        [field: SerializeField] public ResourceType ResourceType { get; private set; }
        [field: SerializeField] public float Amount { get; private set; }

        public void PassResource(ResourceType type, float amount)
        {
            switch (type)
            {
                case ResourceType.Energy:
                    {
                        EventManager.GetEvent<PassResourceToPlayerEvent>().TriggerEvent(ResourceType.Energy, Amount);
                        break;
                    }
                case ResourceType.Health:
                    {
                        EventManager.GetEvent<PassResourceToPlayerEvent>().TriggerEvent(ResourceType.Health, Amount);
                        break;
                    }
            }

        }
    }
}
