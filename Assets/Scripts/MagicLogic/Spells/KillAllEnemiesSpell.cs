using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Spells
{
    public class KillAllEnemiesSpell : ISpellable
    {
        public void Use()
        {
            EventManager.GetEvent<KillAllEnemiesEvent>().TriggerEvent();
        }
    }
}
