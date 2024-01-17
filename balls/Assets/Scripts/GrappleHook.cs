using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;
    public List<Transform> hookPoints = new List<Transform>();

    private Vector2 _closestHook;
    private void Start()
    {
        distanceJoint.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float closestDistance = float.MaxValue;
            foreach (Transform target in hookPoints)
            {
                float distance = Vector2.Distance(transform.position, target.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    _closestHook = target.transform.position;
                }
            }
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, _closestHook);
            distanceJoint.connectedAnchor = _closestHook;
            distanceJoint.enabled = true;
            lineRenderer.enabled = true;
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            distanceJoint.enabled = false;
            lineRenderer.enabled = false;
        }

        if (distanceJoint.enabled)
        {
            lineRenderer.SetPosition(0, transform.position);
        }
    }
}