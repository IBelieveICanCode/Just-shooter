using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Events
{
    public static class EventManager
    {
        private static Container<IEventable> EventsContainer = new Container<IEventable>();

        public static bool IsInitialized = false;

        public static void InitEssentials()
        {
            Debug.Log("EventManager is initiated"); //TODO Make a mediator for consolidation of similar events
            EventsContainer.Add(typeof(AnnouncePlayerPositionEvent), new AnnouncePlayerPositionEvent());
            EventsContainer.Add(typeof(EnemyDeadEvent), new EnemyDeadEvent());
            EventsContainer.Add(typeof(PlayerDeadEvent), new PlayerDeadEvent());
            EventsContainer.Add(typeof(GameIsPausedEvent), new GameIsPausedEvent());
            EventsContainer.Add(typeof(PassResourceToPlayerEvent), new PassResourceToPlayerEvent());
            EventsContainer.Add(typeof(KillAllEnemiesEvent), new KillAllEnemiesEvent());
            IsInitialized = true;
        }

        public static T GetEvent<T>() where T : class, IEventable
        {
            return EventsContainer?.Get<T>();
        }
    }
}
