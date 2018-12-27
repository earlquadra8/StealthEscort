using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class AIWalkerCtrlrBs : MonoBehaviour
{
    public enum ActionState
    {
        Patrolling = 1,
        WaypointAction,
        CutInAction
    }


    [Header ("AIWalkerCtrlrBs")]
    [Header ("Navigation")]
    public Action<int> OnWalkerReachedWaypointEvent;
    public float waitTime = 5;
    public float turningSpeed = 1.75f;
    public float lookFrontTolerance = 0.05f;
    public Transform pathHolder;
    protected NavMeshAgent navMeshAgent;
    protected List<Action<Action>> waypointActions;
    protected Coroutine WalkSingleWaypointCrtnHndl;
    protected Coroutine WaypointActionsCrtnHndl;
    protected ActionState actionState;
        
    #region virtual method
    protected virtual void Init()
    {
        this.navMeshAgent = GetComponent<NavMeshAgent> ();
        OnWalkerReachedWaypointEvent += OnWalkerReachedWaypoint;
    }
    
    protected virtual void StartWalk ()
    {
        WalkSingleWaypoint (0);
    }

    protected virtual void WalkSingleWaypoint ( int waypointIndex )
    {
        if (WalkSingleWaypointCrtnHndl != null) {
            StopCoroutine (WalkSingleWaypointCrtnHndl);
            WalkSingleWaypointCrtnHndl = null;
        }
        WalkSingleWaypointCrtnHndl = StartCoroutine (WalkSingleWaypointCrtn (waypointIndex));
    }

    protected virtual IEnumerator WalkSingleWaypointCrtn (int waypointIndex )
    {
        while (this.navMeshAgent.hasPath) {
            yield return null;
        }
        yield return null;

        if (this.OnWalkerReachedWaypointEvent != null) {
            OnWalkerReachedWaypointEvent (waypointIndex);
        }
    }

    protected virtual void OnWalkerReachedWaypoint( int curWaypointIndex )
    {
        Action onAllFinished = () =>
        {
            WalkSingleWaypoint (curWaypointIndex++);
        };

        if (WaypointActionsCrtnHndl != null) {
            StopCoroutine (WaypointActionsCrtnHndl);
            WaypointActionsCrtnHndl = null;
        }
        WaypointActionsCrtnHndl = StartCoroutine (Utility.RunAllActionsCoroutine (this.waypointActions, onAllFinished));
    }
    #endregion

}
