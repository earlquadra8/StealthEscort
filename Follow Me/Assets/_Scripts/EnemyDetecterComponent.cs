using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetecterComponent : DetecterComponentBase
{
    [Header ("EnemyDetecterComponent")]
    public Light spotlight;

    #region Unity Mono
    private void Start()
    {
        Init ();
    }

    void Update()
    {
        Detection ();
    }
    #endregion

    #region abstract method
    protected override void Init()
    {
        //detectingDist = spotlight.range;
        detectingAngle = spotlight.spotAngle;
    }

    #endregion
}
