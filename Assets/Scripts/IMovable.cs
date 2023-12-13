using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter
{
    public interface IMovable
    {
        void Move(Vector2 direction);
        void Rotate(Vector2 direction);
    }
}
