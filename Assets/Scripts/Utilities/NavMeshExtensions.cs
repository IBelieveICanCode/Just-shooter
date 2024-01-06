using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class NavMeshExtensions
{
    private static float Threshold = 0.2f;
    public static bool IsFarAwayFrom(this NavMeshAgent agent, Vector3 destination)
    {
        if (Vector3.Distance(agent.transform.position, destination) > agent.stoppingDistance - Threshold)
        {
            return true;
        }

        return false;
    }

    public static bool IsTooCloseTo(this NavMeshAgent agent, Vector3 destination)
    {
        if (Vector3.Distance(agent.transform.position, destination) <= agent.stoppingDistance + Threshold)
        {
            return true;
        }

        return false;
    }
}
