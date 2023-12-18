using DG.Tweening;
using StateStuff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Enemy
{
    public class AscendInAirState : State<EnemyAdmin>
    {
        public override void EnterState(EnemyAdmin owner)
        {
            owner.Agent.updatePosition = false;
            Debug.Log($"Current state is {GetType().Name}");
            owner.Transform
                .DOMoveY(owner.MaxHeight, owner.TimeOfFlyingUp)
                .OnComplete(() => owner.StateMachine.ChangeState(new FlyingToThePlayerState()));

        }

        public override void ExitState(EnemyAdmin owner)
        {
        }

        public override void UpdateState(EnemyAdmin owner)
        {
        }
    }
}
