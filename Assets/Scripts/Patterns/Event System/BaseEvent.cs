using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    public class BaseEvents : IEventableNonGeneric
    {
        public Action Listeners { get; set; }

        public void StartListening(Action listener)
        {
            Listeners += listener;
        }

        public void StopListening(Action listener)
        {
            Listeners -= listener;
        }

        public void TriggerEvent()
        {
            Listeners?.Invoke();
        }
    }

    public class BaseEvents<T> : IEventableGeneric<T>
    {
        public Action<T> Listeners { get; set; }

        public void StartListening(Action<T> listener)
        {
            Listeners += listener;
        }

        public void StopListening(Action<T> listener)
        {
            Listeners -= listener;
        }

        public void TriggerEvent(T obj1)
        {
            Listeners?.Invoke(obj1);
        }
    }

    public class BaseEvents<T1, T2> : IEventableGeneric<T1, T2>
    {
        public Action<T1, T2> Listeners { get; set; }

        public void StartListening(Action<T1, T2> listener)
        {
            Listeners += listener;
        }

        public void StopListening(Action<T1, T2> listener)
        {
            Listeners -= listener;
        }

        public void TriggerEvent(T1 obj1, T2 obj2)
        {
            Listeners?.Invoke(obj1, obj2);
        }
    }
}
