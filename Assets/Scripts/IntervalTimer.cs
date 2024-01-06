using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Timers
{
    public class IntervalTimer
    {
        private Timer _intervalTimer;
        private float _startingDuration;
        private float _durationDecrease;
        private float _minTriggerDuration;

        private Action _onTimerFire;

        public IntervalTimer(float startingDuration, Action onTimerFire, float durationDecrease = 1, float minDuration = 1)
        {
            _intervalTimer = new Timer();
            _startingDuration = startingDuration;
            _durationDecrease = durationDecrease;
            _minTriggerDuration = minDuration;
            _onTimerFire = onTimerFire;
            StartTimer(_startingDuration);
        }

        private void StartTimer(float duration)
        {
            _intervalTimer.StartTimer(duration, OnTimerCompleted);
        }

        private void OnTimerCompleted()
        {
            _onTimerFire?.Invoke();
            UpdateTimerDuration();
            StartTimer(_startingDuration);
        }

        private void UpdateTimerDuration()
        {
            _startingDuration = Mathf.Max(_minTriggerDuration, _startingDuration - _durationDecrease);
        }

        public void Pause()
        {
            _intervalTimer.PauseTimer();
        }

        public void Resume()
        {
            _intervalTimer.ResumeTimer();
        }
    }
}
