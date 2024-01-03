using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Timers
{
    public class IntervalTimer : MonoBehaviour
    {
        private Timer spawnTimer;
        private float _startingSpawnDuration;
        private float _spawnDurationDecrease;
        private float _minSpawnDuration;

        private Action _onTimerFire;

        public IntervalTimer(float startingSpawnDuration, Action onTimerFire, float spawnDurationDecrease = 1, float minSpawnDuration = 1)
        {
            spawnTimer = new Timer();
            _startingSpawnDuration = startingSpawnDuration;
            _spawnDurationDecrease = spawnDurationDecrease;
            _minSpawnDuration = minSpawnDuration;
            _onTimerFire = onTimerFire;
            StartSpawnTimer(_startingSpawnDuration);
        }

        private void StartSpawnTimer(float duration)
        {
            Debug.Log("Start timer for enemies");
            spawnTimer.StartTimer(duration, OnTimerCompleted);
        }

        private void OnTimerCompleted()
        {
            Debug.Log($"Completed timer with {_startingSpawnDuration} interval");
            _onTimerFire?.Invoke();
            UpdateTimerDuration();
            StartSpawnTimer(_startingSpawnDuration);
        }

        private void UpdateTimerDuration()
        {
            _startingSpawnDuration = Mathf.Max(_minSpawnDuration, _startingSpawnDuration - _spawnDurationDecrease);
        }

        private void StopSpawning()
        {
            spawnTimer.StopTimer();
        }
    }
}
