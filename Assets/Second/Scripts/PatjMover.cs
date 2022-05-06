using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatjMover : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Queue<Vector3> pathPoints = new Queue<Vector3>();

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        FindObjectOfType<PathCreator>().OnNewPathCreated += SetPoints;
    }

    private void SetPoints(IEnumerable<Vector3> obj)
    {
        pathPoints = new Queue<Vector3>(obj);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePathing();
    }

    private void UpdatePathing()
    {
        if (ShouldSetDestination())
        {
            _agent.SetDestination(pathPoints.Dequeue());
        }
    }

    private bool ShouldSetDestination()
    {
        if (pathPoints.Count == 0)
        {
            return false;
        }

        if (_agent.hasPath == false || _agent.remainingDistance < 0.5f)
        {
            return true;
        }

        return false;
    }
}