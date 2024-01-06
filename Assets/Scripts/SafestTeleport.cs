using System.Collections;
using System.Collections.Generic;
using TestShooter.Enemy;
using UnityEngine;

namespace TestShooter.Teleport
{
    public class SafestTeleport : ITeleportMechanicable
    {
        private float _radius;
        private int _amountOfPoints;

        public SafestTeleport(float radius, int amountOfPoints)
        {
            _radius = radius;
            _amountOfPoints = amountOfPoints;
        }

        public void Teleport(Transform target)
        {
            IEnemyable[] enemies = Utilities.GetEnemiesInRadius(Vector3.zero, _radius);

            Vector3 bestLocation = Vector3.zero;
            float maxDistance = 0f;

            for (int i = 0; i < _amountOfPoints; i++)
            {
                Vector3 randomDirection = Random.insideUnitSphere * _radius;
                UnityEngine.AI.NavMeshHit hit;
                if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, _radius, UnityEngine.AI.NavMesh.AllAreas))
                {
                    Vector3 potentialLocation = hit.position;
                    float minDistanceToEnemies = float.MaxValue;

                    foreach (var enemy in enemies)
                    {
                        float distance = Vector3.Distance(potentialLocation, enemy.Transform.position);
                        minDistanceToEnemies = Mathf.Min(minDistanceToEnemies, distance);
                    }

                    if (minDistanceToEnemies > maxDistance)
                    {
                        maxDistance = minDistanceToEnemies;
                        bestLocation = potentialLocation;
                    }
                }
            }

            if (bestLocation != Vector3.zero)
            {
                target.position = bestLocation;
            }
        }
    }
}
