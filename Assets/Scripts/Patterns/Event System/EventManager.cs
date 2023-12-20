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

        public static void Init()
        {
            Debug.Log("EventManager is initiated");
            EventsContainer.Add(typeof(AnnouncePlayerPosition), new AnnouncePlayerPosition());
            IsInitialized = true;
        }

        public static T GetEvent<T>() where T : class, IEventable
        {
            return EventsContainer?.Get<T>();
        }
    }
}
