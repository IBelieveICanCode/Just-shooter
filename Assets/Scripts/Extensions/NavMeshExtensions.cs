using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class NavMeshExtensions
{
    public static bool IsFarAwayFrom(this NavMeshAgent agent, Vector3 destination, float threshold)
    {
        if (Vector3.Distance(agent.transform.position, destination) > agent.stoppingDistance - threshold)
        {
            return true;
        }

        return false;
    }

    public static bool IsTooCloseTo(this NavMeshAgent agent, Vector3 destination, float threshold)
    {
        if (Vector3.Distance(agent.transform.position, destination) <= agent.stoppingDistance + threshold)
        {
            return true;
        }

        return false;
    }
}
