using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateStuff
{
    public class StateMachine<T> where T: MonoBehaviour
    {
        public State<T> CurrentState { get; private set; }
        public T Owner;

        public StateMachine(T o)
        {
            Owner = o;
            CurrentState = null;
        }

        public void ChangeState(State<T> newstate)
        {
            if (CurrentState != null)
                CurrentState.ExitState(Owner);
            CurrentState = newstate;
            CurrentState.EnterState(Owner);
        }

        public void Update()
        {
            if (CurrentState != null)
                CurrentState.UpdateState(Owner);
        }
    }

    public abstract class State<T>
    {
        public abstract void EnterState(T owner);
        public abstract void ExitState(T owner);
        public abstract void UpdateState(T owner);
    }
}
