using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Player
{
    public interface IPlayerDetectable 
    {
        public Transform Transform { get; }
    }
}
