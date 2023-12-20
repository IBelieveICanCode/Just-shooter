using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TestShooter.Enemy
{
    public class EnemySpawnPositionFinder
    {
        private float range = 10.0f; // TODO Calcualte range depending on the size of nav mesh surface

        public Vector3 CalculateSpawnPoint(Transform transform)
        {
            Vector3 randomPoint = GetRandomPointOnNavMesh(transform.position, range);

            if (randomPoint != Vector3.zero)
            {
                return randomPoint;
            }

            Debug.LogError("Failed to calculate position on NavMeshSurface. It is either too far away or the surfaace isn't valid");
            return randomPoint;
        }

        Vector3 GetRandomPointOnNavMesh(Vector3 center, float range)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
            {
                return hit.position;
            }

            return Vector3.zero; // Return zero if no point is found
        }
    }
}
