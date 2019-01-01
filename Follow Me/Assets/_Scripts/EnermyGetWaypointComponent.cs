using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyGetWaypointComponent : GetWaypointComponentBase
{
    [Header ("EnermyGetWaypointComponent")]
    public Transform waypointsHolder;

    #region abstract method
    public override void Init()
    {

    }
    public override Transform GetNextWaypoint()
    {
        Transform nextWaypoint = waypointsHolder.GetChild (0);
        nextWaypoint.SetAsLastSibling ();
        return nextWaypoint;
    }
    #endregion
    
    public Transform GetClosestWaypoint()
    {
        Transform closestWaypoint = PeekNextWayPoint ();
        float shortestDist = float.MaxValue;
        foreach (Transform waypoint in waypointsHolder) {
            float dist = Vector3.Distance (waypoint.position, transform.position);
            if (dist < shortestDist) {
                shortestDist = dist;
                closestWaypoint = waypoint;
            }
        }

        return closestWaypoint;
    }

    public Transform PeekNextWayPoint()
    {
        return waypointsHolder.GetChild (0);
    }

    private void OnDrawGizmos()     {         Vector3 startPos = waypointsHolder.GetChild (0).position;         Vector3 previousPos = startPos;          foreach (Transform waypoint in waypointsHolder) {             Gizmos.color = Color.red;             Gizmos.DrawSphere (waypoint.position, 0.25f);             Gizmos.color = Color.white;             Gizmos.DrawLine (previousPos, waypoint.position);             previousPos = waypoint.position;             Gizmos.color = Color.magenta;             Gizmos.DrawSphere (waypoint.GetChild (0).position, 0.05f);             Gizmos.color = Color.cyan;             Gizmos.DrawSphere (waypoint.GetChild (1).position, 0.05f);         }         Gizmos.color = Color.green;         Gizmos.DrawLine (previousPos, startPos);     }
}
