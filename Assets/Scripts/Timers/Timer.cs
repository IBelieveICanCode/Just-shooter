using UniRx;
using System;

namespace TestShooter.Timers
{
    public class Timer
    {
        private float _remainingDuration = 0f;
        private DateTime _pauseTime;
        private Action _onCompleteAction;

        private IDisposable _timerSubscription;
        private readonly ReactiveProperty<bool> _isTimerActive = new ReactiveProperty<bool>(false);
        public IReadOnlyReactiveProperty<bool> IsTimerActive => _isTimerActive;

        public bool IsCompleted { get; private set; }

        public void StartTimer(float duration)
        {
            _remainingDuration = duration;
            StartOrResumeTimer();
        }

        public void StartTimer(float duration, Action onCompleteAction)
        {
            _remainingDuration = duration;
            _onCompleteAction = onCompleteAction;
            StartOrResumeTimer();
        }

        private void StartOrResumeTimer()
        {
            IsCompleted = false;

            if (_isTimerActive.Value)
            {
                return;
            }

            _isTimerActive.Value = true;
            _timerSubscription = Observable.Timer(TimeSpan.FromSeconds(_remainingDuration))
                      .Subscribe(_ =>
                      {
                          _isTimerActive.Value = false;
                          IsCompleted = true;
                          _onCompleteAction?.Invoke();
                      });
        }

        public void StopTimer()
        {
            if (_isTimerActive.Value && _timerSubscription != null)
            {
                _timerSubscription.Dispose();
                _isTimerActive.Value = false;
                IsCompleted = true;
            }
        }

        public void PauseTimer()
        {
            if (_isTimerActive.Value && _timerSubscription != null)
            {
                _pauseTime = DateTime.Now;
                _timerSubscription.Dispose();
                _isTimerActive.Value = false;
            }
        }

        public void ResumeTimer()
        {
            if (!IsCompleted && !_isTimerActive.Value)
            {
                var timePaused = DateTime.Now - _pauseTime;
                _remainingDuration -= (float)timePaused.TotalSeconds;
                StartOrResumeTimer();
            }
        }
    }
}