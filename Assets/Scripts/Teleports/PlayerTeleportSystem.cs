using System.Collections;
using System.Collections.Generic;
using TestShooter.Enemy;
using TestShooter.Player;
using TestShooter.Timers;
using UnityEngine;

namespace TestShooter.Teleport
{
    public class PlayerTeleportSystem : MonoBehaviour
    {
        [SerializeField] private int _teleportCooldown = 3;
        [SerializeField] private TeleportCollider[] _teleportColliders;
        private Timer _teleportTimer = new Timer();
        private ITeleportMechanicable _teleportLogic;

        private const int Radius = 7;
        private const int AmountOfPointToCheck = 10;

        private void Start()
        {
            foreach (var teleport in _teleportColliders)
            {
                teleport.OnTeleportColliderEnter += TriggerTeleport;
            }

            _teleportLogic = new SafestTeleport(Radius, AmountOfPointToCheck);
        }

        private void TriggerTeleport(Collider collider)
        {
            if (_teleportTimer.IsTimerActive.Value)
            {
                return;
            }

            IPlayerDetectable playerToTeleport = GetPlayerTeleporting(collider);

            if (playerToTeleport == null)
            {
                return;
            }

            _teleportTimer.StartTimer(_teleportCooldown);
            _teleportLogic.Teleport(playerToTeleport.Transform);    
        }

        private IPlayerDetectable GetPlayerTeleporting(Collider collider)
        {
            return collider.GetComponent<IPlayerDetectable>();
        }

        private void OnDestroy()
        {
            foreach (var teleport in _teleportColliders)
            {
                teleport.OnTeleportColliderEnter -= TriggerTeleport;
            }

            _teleportTimer.StopTimer();
        }
    }
}
