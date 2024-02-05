using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Events;
using TestShooter.Player;

namespace TestShooter.GameCamera
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class GameCameraAdmin : MonoBehaviour
    {
        private CinemachineVirtualCamera _virtualCamera;

        private void Awake()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            EventManager.GetEvent<AnnouncePlayerPositionEvent>().StartListening(SetVirtualCameraFollow);
        }

        private void SetVirtualCameraFollow(IPlayerDetectable player)
        {
            _virtualCamera.Follow = player.Transform;
        }
    }
}
