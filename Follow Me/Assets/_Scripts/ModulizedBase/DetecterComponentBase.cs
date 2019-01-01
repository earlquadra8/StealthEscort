using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DetecterComponentBase : MonoBehaviour
{
    [Header ("DetecterComponentBase")]
    public DetectableManager detectableManager;
    public float detectingDist;
    public float detectingAngle;
    public float detectCounterMax;
    private float detectCounter;
    private DetectableComponetBase lastDetectedTarget;
    private DetectableComponetBase firstDetectedTarget;
    
    #region abstract method
    protected abstract void Init();
    #endregion

    #region virtual method
    protected virtual void Detection()
    {
        bool hasDetected = false;
        foreach (DetectableComponetBase detectable in detectableManager.detectables) {
            float dist = Vector3.Distance (detectable.transform.position, transform.position);
            if (dist <= detectingDist) {
                Vector3 displace = detectable.transform.position - transform.position;
                if (Vector3.Angle (transform.forward, displace) <= detectingAngle) {

                    if (firstDetectedTarget == null) {
                        firstDetectedTarget = detectable;
                    }
                    hasDetected = true;
                    lastDetectedTarget = detectable;

                }

            }
        }

        if (!hasDetected) {
            ClearDetection ();
        }
    }

    protected virtual void ClearDetection()
    {
        firstDetectedTarget = null;
        lastDetectedTarget = null;
    }
    #endregion
}
