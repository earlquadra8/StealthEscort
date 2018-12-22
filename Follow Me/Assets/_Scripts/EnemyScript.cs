﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [Header("Navigation")]
    public float waitTime = 2f;
    public float turningSpeed = 120f;
    public float lookFrontTolerance = 0.05f;
    public Transform pathHolder;
    [Header("Detection")]
    public float detectingDistance = 30;

    #region Navigation
    NavMeshAgent _nav;
    Vector3[] _waypoints;
    Vector3[] _scanA;
    Vector3[] _scanB;
    Vector3 _nextA;
    Vector3 _nextB;
    int _patrolNodeIndex;// where this enemy is now patrolling to
    float _timer;
    bool _hasLookedA;
    #endregion Navigation

    #region Detection
    Transform[] _detectees;
    Transform _detectedTarget;
    bool _hasDetection;
    float _detectionAngle;
    #endregion Detection

    private void Start()
    {
        _nav = GetComponent<NavMeshAgent>();
        _waypoints = new Vector3[pathHolder.childCount];
        _scanA = new Vector3[_waypoints.Length];
        _scanB = new Vector3[_waypoints.Length];
        for (int i = 0; i < _waypoints.Length; i++) // get the position of the waypoint emptyObject
        {
            Transform waypoint = pathHolder.GetChild(i);
            _waypoints[i] = waypoint.position;
            _scanA[i] = waypoint.GetChild(0).position;
            _scanB[i] = waypoint.GetChild(1).position;
        }

        _patrolNodeIndex = 0;
        _timer += waitTime;// allow getting destination at start, or set a start wait time.
        _hasLookedA = false;

        _detectees = new Transform[2];
        _detectees[0] = GameObject.Find("Player").transform;
        _detectees[1] = GameObject.Find("VIP").transform;
        _detectionAngle = gameObject.GetComponentInChildren<Light>().spotAngle * 0.5f;
    }
    private void Update()
    {
        Patrol();
        DetectPlayer();
    }

    public void Patrol()
    {
        if (!_nav.hasPath)// arrived, _nav.hasPath will return false before any destination is asigned to the agent
        {
            if (_timer >= waitTime)// it triggers once per waypoint
            {
                _timer = 0;
                _hasLookedA = false;
                _nav.SetDestination(_waypoints[_patrolNodeIndex]);// set new destination
                _nextA = _scanA[_patrolNodeIndex];
                _nextB = _scanB[_patrolNodeIndex];
                if (_patrolNodeIndex < _waypoints.Length - 1)
                {
                    _patrolNodeIndex++;// get next position
                }
                else
                {
                    _patrolNodeIndex = 0;// back to the first node
                }
            }
            else// it triggers many times per waypoint
            {
                _timer += Time.deltaTime;
                Scanning();
            }
        }
    }

    public void Scanning()
    {

        Vector3 targetDirA = new Vector3(_nextA.x, transform.position.y, _nextA.z);
        Vector3 targetDirB = new Vector3(_nextB.x, transform.position.y, _nextB.z);
        if (Vector3.Angle(transform.forward, targetDirA - transform.position) > lookFrontTolerance && !_hasLookedA)
        {
            transform.forward = Vector3.Lerp(transform.forward, targetDirA - transform.position, turningSpeed * Time.deltaTime);
        }
        else
        {
            _hasLookedA = true;
            transform.forward = Vector3.Lerp(transform.forward, targetDirB - transform.position, turningSpeed * Time.deltaTime);
        }
    }

    public void DetectPlayer()
    {
        foreach (Transform detectee in _detectees)
        {
            Vector3 displace = detectee.position - transform.position;
            Ray ray = new Ray(transform.position, displace);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, detectingDistance))
            {
                if (Vector3.Angle(transform.forward, displace) <= _detectionAngle)
                {
                    if (hitInfo.transform.tag == "Player")
                    {
                        _hasDetection = true;
                        _detectedTarget = detectee;
                        print(_detectedTarget.name);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 startPos = pathHolder.GetChild(0).position;
        Vector3 previousPos = startPos;

        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(waypoint.position, 0.25f);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(previousPos, waypoint.position);
            previousPos = waypoint.position;
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(waypoint.GetChild(0).position, 0.05f);
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(waypoint.GetChild(1).position, 0.05f);
        }
        Gizmos.color = Color.green;
        Gizmos.DrawLine(previousPos, startPos);
    }
}