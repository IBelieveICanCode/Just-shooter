using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.UI
{
    public class UIPlatformMediator : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _keyboardGroup;
        [SerializeField] private CanvasGroup _joyStickGroup;

        void Start()
        {
            _keyboardGroup.alpha = 0;
            _joyStickGroup.alpha = 0;

            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                _joyStickGroup.alpha = 1;
            }
            else
            {
                _keyboardGroup.alpha = 1;
            }
        }
    }
}
