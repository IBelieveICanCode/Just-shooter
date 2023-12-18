using UniRx;
using System;

namespace TestShooter.Timers
{
    public class Timer
    {
        private readonly ReactiveProperty<bool> _isTimerActive = new ReactiveProperty<bool>(false);
        public IReadOnlyReactiveProperty<bool> IsTimerActive => _isTimerActive;

        public bool IsCompleted { get; private set; }

        public void StartTimer(float duration)
        {
            IsCompleted = false;

            if (_isTimerActive.Value)
            {
                return;
            }

            _isTimerActive.Value = true;
            Observable.Timer(TimeSpan.FromSeconds(duration))
                      .Subscribe(_ =>
                      {
                          _isTimerActive.Value = false;
                          IsCompleted = true;
                      });
        }

        public void StartTimer(float duration, Action onCompleteAction)
        {
            IsCompleted = false;

            if (_isTimerActive.Value)
            {
                return;
            }

            _isTimerActive.Value = true;
            Observable.Timer(TimeSpan.FromSeconds(duration))
                      .Subscribe(_ =>
                      {
                          _isTimerActive.Value = false;
                          IsCompleted = true;
                          onCompleteAction?.Invoke(); // Invoke the passed method
                      });
        }
    }
}