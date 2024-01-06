using StateStuff;
using System.Collections;
using System.Collections.Generic;
using TestShooter.Timers;
using UnityEngine;

namespace TestShooter.Enemy
{
    public class StopState<T> : State<T> where T: MonoBehaviour
    {
        private StateMachine<T> _stateMachine;
        private State<T> _nextState;
        private float _stopDuration;

        private Timer _timer;

        public StopState(StateMachine<T> stateMachine, State<T> nextState, float stopDuration)
        {
            _stateMachine = stateMachine;
            _nextState = nextState;
            _stopDuration = stopDuration;
        }

        public override void EnterState(T owner)
        {
            _timer = new Timer();
            _timer.StartTimer(_stopDuration, () => _stateMachine.ChangeState(_nextState));
        }

        public override void ExitState(T owner)
        {
        }

        public override void UpdateState(T owner)
        {
        }
    }
}
