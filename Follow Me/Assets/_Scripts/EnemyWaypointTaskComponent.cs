using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyWaypointTaskComponent : WaypointTaskComponentBase
{
    [Header ("EnemyWaypointTaskComponent")]
    public float scanningTurnSpeed = 1.75f;
    public float scanningTolerance = 0.05f;
    private Coroutine RotateToScanPointCrtnHndl;

    #region virtual method
    public override void Init()
    {
        base.Init ();

    }
    #endregion

    public void RotateToScanPoint( Transform waypoint, Action onFinished )
    {
        if (RotateToScanPointCrtnHndl != null) {
            StopCoroutine (RotateToScanPointCrtnHndl);
            RotateToScanPointCrtnHndl = null;
        }
        RotateToScanPointCrtnHndl = StartCoroutine (RotateToScanPointCrtn (waypoint, onFinished));
    }
    private IEnumerator RotateToScanPointCrtn( Transform waypoint, Action onFinished )
    {
        foreach (Transform scanPoint in waypoint) {
            Vector3 toLookDir = new Vector3 (scanPoint.position.x, transform.position.y, scanPoint.position.z) - transform.position;
            while (Vector3.Angle (transform.forward, toLookDir) >= scanningTolerance) {
                transform.forward = Vector3.Lerp (transform.forward, toLookDir, scanningTurnSpeed * Time.deltaTime);
                yield return null;
            }
        }
        yield return null;
        if (onFinished != null) {
            onFinished ();
        }
    }
}
