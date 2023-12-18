using StateStuff;
using System.Collections;
using System.Collections.Generic;
using TestShooter.Timers;
using UnityEngine;

namespace TestShooter.Enemy
{
    public class StopState : State<EnemyAdmin>
    {
        private State<EnemyAdmin> _nextState;
        private float _stopDuration;

        private Timer _timer;

        public StopState(State<EnemyAdmin> nextState, float stopDuration)
        {
            _nextState = nextState;
            _stopDuration = stopDuration;
        }

        public override void EnterState(EnemyAdmin owner)
        {
            Debug.Log($"Current state is {GetType().Name}");

            _timer = new Timer();
            _timer.StartTimer(_stopDuration, () => owner.StateMachine.ChangeState(_nextState));
        }

        public override void ExitState(EnemyAdmin owner)
        {
        }

        public override void UpdateState(EnemyAdmin owner)
        {
        }
    }
}
