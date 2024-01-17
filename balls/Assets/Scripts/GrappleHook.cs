using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;

    public List<Transform> hookPoints = new List<Transform>();

    private void Start()
    {
        distanceJoint.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Transform target in hookPoints)
            {
                
            }
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, mousePos);
            distanceJoint.connectedAnchor = mousePos;
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