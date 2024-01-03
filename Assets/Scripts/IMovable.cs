using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter
{
    public interface IMovable
    {
        void Move(Vector3 direction);
    }
}
