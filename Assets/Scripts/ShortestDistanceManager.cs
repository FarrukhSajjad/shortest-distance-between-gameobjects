using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShortestDistanceManager : MonoBehaviour
{
    public Camera myCamera;
    public Transform target;
    public NavMeshAgent agent;
    public LineRenderer line;

    private void Start()
    {
        GetPath();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = myCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                target.gameObject.GetComponent<NavMeshAgent>().SetDestination(hit.point);
            }
        }
    }


    private void LateUpdate()
    {
        GetPath();
    }

    private void GetPath()
    {
        line.SetPosition(0, agent.gameObject.transform.position);
        agent.SetDestination(target.position);
        DrawPath(agent.path);
        agent.isStopped = true;
    }

    private void DrawPath(NavMeshPath path)
    {
        if (path.corners.Length < 2)
            return;

        line.positionCount = path.corners.Length;

        for (var i = 1; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]);
        }
    }
}
