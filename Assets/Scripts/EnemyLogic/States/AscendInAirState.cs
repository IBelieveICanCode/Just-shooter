using DG.Tweening;
using StateStuff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Enemy
{
    public class AscendInAirState : State<FlyingEnemy>
    {
        public override void EnterState(FlyingEnemy owner)
        {
            owner.Agent.updatePosition = false;
            owner.Transform
                .DOMoveY(owner.MaxHeightOfFlying, owner.TimeOfFlyingUp)
                .OnComplete(() => owner.StateMachine.ChangeState(new FlyingToThePlayerState()));

        }

        public override void ExitState(FlyingEnemy owner)
        {
        }

        public override void UpdateState(FlyingEnemy owner)
        {
        }
    }
}
