using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Buffs
{
    public interface IBuffable
    {
        BuffTypes BuffType { get; }
        void Buff();
        void ResetBuff();
        void Dispose();
    }
}
