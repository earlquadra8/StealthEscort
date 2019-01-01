using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent (typeof (GetWaypointComponentBase))]
public abstract class WaypointTaskComponentBase : MonoBehaviour
{
    protected GetWaypointComponentBase waypointComponent;
    protected Coroutine WaypointActionsCrtnHndl;
    protected List<Action<Action>> waypointTasks = new List<Action<Action>> ();

    #region abstract method
    #endregion

    #region virtual method
    public virtual void Init()
    {
        waypointComponent = GetComponent<GetWaypointComponentBase> ();
    } 

    public virtual void StartReachedWaypointTask ( Action onAllFinished )
    {
        if (WaypointActionsCrtnHndl != null) {
            StopCoroutine (WaypointActionsCrtnHndl);
            WaypointActionsCrtnHndl = null;
        }
        WaypointActionsCrtnHndl = StartCoroutine (Utility.RunAllActionsCoroutine (this.waypointTasks, onAllFinished));
    }
    #endregion
}
