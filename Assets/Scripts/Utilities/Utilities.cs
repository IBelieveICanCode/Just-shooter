using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TestShooter.Enemy;
using UnityEngine;

namespace TestShooter
{
    public static class Utilities
    {
        public const string EnemyLayer = "Enemy";
        public const string Playerlayer = "Player";

        public static IEnemyable[] GetEnemiesInRadius(Vector3 fromPosition, float radius)
        {
            int layer = LayerMask.NameToLayer(EnemyLayer);
            LayerMask layerMask = 1 << layer;
            Collider[] hitColliders = Physics.OverlapSphere(fromPosition, radius, layerMask);
            return hitColliders
                        .Select(collider => collider.GetComponent<IEnemyable>())
                        .Where(enemy => enemy != null)
                        .ToArray();
        }

        public static float WrapAngle(float angle)
        {
            angle %= 360;
            if (angle > 180)
                return angle - 360;

            return angle;
        }
    }
}
