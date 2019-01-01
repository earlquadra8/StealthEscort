using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent), typeof (GetWaypointComponentBase))]
public abstract class WalkableComponentBase : MonoBehaviour
{
    [Header ("WalkableComponentBase")]
    protected NavMeshAgent navMeshAgent;
    protected GetWaypointComponentBase waypointComponent;
    protected Coroutine walkSingleWaypointCrtnHndl;

    #region abstract method
    protected abstract void OnWalkableReachedAWaypoint();
    #endregion

    #region virtual method
    protected virtual void Init()
    {
        this.navMeshAgent = GetComponent<NavMeshAgent> ();
        this.waypointComponent = GetComponent<GetWaypointComponentBase> ();
        //this.OnWalkableReachedAWaypointEvent += OnWalkableReachedAWaypoint;
    }

    protected virtual Transform GetNextWayPoint()
    {
        return this.waypointComponent.GetNextWaypoint ();
    }

    protected virtual void WalkToSingleWaypoint(Vector3 targetWaypoint, Action onWalkFinished = null)
    {
        if (walkSingleWaypointCrtnHndl != null) {
            StopCoroutine (walkSingleWaypointCrtnHndl);
            walkSingleWaypointCrtnHndl = null;
        }
        walkSingleWaypointCrtnHndl = StartCoroutine (WalkSingleWaypointCrtn (targetWaypoint, onWalkFinished));
    }

    protected virtual IEnumerator WalkSingleWaypointCrtn( Vector3 targetWaypoint, Action onWalkFinished )
    {
        this.navMeshAgent.SetDestination (targetWaypoint);
        yield return null;
        while (this.navMeshAgent.hasPath) {
            yield return null;
        }
        yield return null;
        if (onWalkFinished != null) {
            onWalkFinished ();
        }
    }

    protected virtual void InterruptWalk()
    {
        if (walkSingleWaypointCrtnHndl != null) {
            StopCoroutine (walkSingleWaypointCrtnHndl);
            walkSingleWaypointCrtnHndl = null;
        }
    }
    #endregion
}
