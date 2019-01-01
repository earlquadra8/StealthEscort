using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(GetWaypointComponentBase))]
public abstract class GetWaypointComponentBase : MonoBehaviour
{
    //[Header("GetWaypointComponentBase")]
    #region abstract method
    public abstract void Init();
    public abstract Transform GetNextWaypoint();
    #endregion
}
