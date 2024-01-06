using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Enemy
{
    public interface ILootable
    {
        void PassResource(ResourceType type, float amount);
    }
}
