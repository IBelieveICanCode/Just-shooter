using DG.Tweening;
using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestShooter.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private float _durationOfShowHide = 1f;
        [SerializeField] private CanvasGroup _mainMenuCanvasGroup;

        [SerializeField] private Button _startOverButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _quitButton;

        private void Start()
        {
            Hide(0);

            EventManager.GetEvent<GameIsPausedEvent>().StartListening(OnPause);
            EventManager.GetEvent<GameOverEvent>().StartListening(OnPlayerDead);

            _startOverButton.onClick.AddListener(OnStartOverButtonClicked);
            _resumeButton.onClick.AddListener(OnResumeButtonClicked);
            _quitButton.onClick.AddListener(OnQuitButtonClicked);
        }

        private void OnPause(bool pause)
        {
            if (pause)
            {
                Show(_durationOfShowHide);
                return;
            }

            Hide(_durationOfShowHide);
        }

        private void OnPlayerDead()
        {
            _resumeButton.interactable = false;
            EventManager.GetEvent<GameIsPausedEvent>().TriggerEvent(true);
        }

        private void OnStartOverButtonClicked()
        {
            _resumeButton.interactable = true;
            EventManager.GetEvent<StartGameEvent>().TriggerEvent();
        }

        private void OnResumeButtonClicked()
        {
            EventManager.GetEvent<GameIsPausedEvent>().TriggerEvent(false);
        }

        private void OnQuitButtonClicked()
        {
            Application.Quit();
        }

        private void Show(float duration)
        {
            _mainMenuCanvasGroup.DOFade(1, duration);
            _mainMenuCanvasGroup.transform.DOScale(Vector3.one, duration).SetEase(Ease.OutBack);
        }

        private void Hide(float duration)
        {
            _mainMenuCanvasGroup.DOFade(0, duration);
            _mainMenuCanvasGroup.transform.DOScale(Vector3.zero, duration).SetEase(Ease.OutBack);
        }
    }
}
