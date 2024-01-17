using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;

    private void Start()
    {
        distanceJoint.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            distanceJoint.connectedAnchor = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
            distanceJoint.enabled = true;
        }
        else
        {
            distanceJoint.enabled = false;
        }
    }
}