using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class EnermyWalkableComponent : WalkableComponentBase
{
    [Header ("EnermyWalkableComponent")]
    private EnemyWaypointTaskComponent waypointTask;
    #region Unity Mono
    void Start()
    {
        Init ();
        waypointComponent.Init ();

        StartWalk ();
    }

    void OnDestroy()
    {
       
    }
    #endregion

    #region abstract method
    protected override void OnWalkableReachedAWaypoint()
    {

    }
    #endregion

    #region virtual method
    protected override void Init()
    {
        base.Init ();
        this.waypointTask = GetComponent<EnemyWaypointTaskComponent> ();
    }
    #endregion

    private void StartWalk ()
    {
        WalkLoop ();
    }

    private void WalkLoop ()
    {
        Transform nextWaypoint = GetNextWayPoint ();
        WalkToSingleWaypoint (nextWaypoint.position, () => waypointTask.RotateToScanPoint (nextWaypoint, () => WalkLoop ()));
    }
}
