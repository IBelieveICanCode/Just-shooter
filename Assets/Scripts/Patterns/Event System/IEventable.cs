using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    public interface IEventable
    {

    }

    public interface IEventableNonGeneric : IEventable
    {
        Action Listeners { get; set; }
        void StartListening(Action listener);
        void StopListening(Action listener);
        void TriggerEvent();
    }

    public interface IEventableGeneric<T1> : IEventable
    {
        Action<T1> Listeners { get; set; }
        void StartListening(Action<T1> listener);
        void StopListening(Action<T1> listener);
        void TriggerEvent(T1 obj1);
    }

    public interface IEventableGeneric<T1, T2> : IEventable
    {
        Action<T1, T2> Listeners { get; set; }
        void StartListening(Action<T1, T2> listener);
        void StopListening(Action<T1, T2> listener);
        void TriggerEvent(T1 obj1, T2 obj2);
    }
}
