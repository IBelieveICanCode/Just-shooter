using StateStuff;
using System.Collections;
using System.Collections.Generic;
using TestShooter.Shooting;
using UnityEngine;

namespace TestShooter.Enemy
{
    public class StandAndShootThePlayerState : State<ShootingEnemy>
    {
        private IWeaponable _currentWeapon;
        private IRotatable _rotation;

        public override void EnterState(ShootingEnemy owner)
        {
            _currentWeapon = owner.Gun;
            _rotation = new EnemyBasicRotation(owner.Transform);
        }

        public override void ExitState(ShootingEnemy owner)
        {
        }

        public override void UpdateState(ShootingEnemy owner)
        {
            if (owner.PlayerTransform == null)
            {
                return;
            }

            _currentWeapon.Fire();
            _rotation.Rotate(owner.PlayerTransform.position);

            if (owner.Agent.IsFarAwayFrom(owner.PlayerTransform.position))
            {
                owner.StateMachine.ChangeState(new FindAndShootThePlayerState());
            }
        }
    }
}
