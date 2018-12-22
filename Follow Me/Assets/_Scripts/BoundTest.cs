using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundTest : MonoBehaviour
{
    public GameObject target;
    public GameObject indicator;
    public Vector3 vector;
    public float viewDist;
    Vector3 _testVector;
    Transform _testTransform;

    Vector3 targetDist;
    Vector3 targetDir;
    Vector3 targetRelativeLeftDir;
    Vector3 targetRelativeRightDir;
    Vector3 targetRelativeUpDir;
    Vector3 targetRelativeDownDir;
    Vector3 boundPoint;

    void Start ()
    {
        //indicator.transform.position = transform.TransformPoint(transform.GetComponent<MeshFilter>().mesh.bounds.min);
        

    }

    void Update ()
    {
        //print("Transform Dist: " + (transform.position - target.transform.position).sqrMagnitude);
        //print("Bounds Dist: " + target.GetComponent<MeshFilter>().mesh.bounds.SqrDistance(target.transform.localPosition));
        //print("Target mesh (Local): "+target.GetComponent<MeshFilter>().mesh.bounds.size);
        //print("Target renderer (World): " + target.GetComponent<Renderer>().bounds.size.magnitude);
        //print("Target collider (World): " + target.GetComponent<Collider>().bounds.size);
        //print(target.GetComponent<MeshFilter>().mesh.bounds.ClosestPoint(target.transform.position + new Vector3(0.2f, 0, 0)));
        //print(target.transform.position);
        //
        //targetDist = target.transform.position - transform.position;
        //targetDir = targetDist.normalized;
        //targetRelativeLeftDir = new Vector3(-targetDir.z, targetDir.y, targetDir.x);
        //targetRelativeRightDir = new Vector3(targetDir.z, targetDir.y, -targetDir.x);
        //targetRelativeUpDir = new Vector3(targetDir.x, targetDir.z, -targetDir.y);
        //targetRelativeDownDir = new Vector3(targetDir.x, -targetDir.z, targetDir.y);
        //
        //boundPoint = target.GetComponent<Collider>().bounds.ClosestPoint(target.transform.position + (targetRelativeRightDir * 3));
        //indicator.transform.position = boundPoint;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, target.GetComponent<Collider>().bounds.ClosestPoint(transform.position));
    }

    void WhatIWant()
    {

    }
}
