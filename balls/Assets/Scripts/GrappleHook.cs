using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using FishNet;
using FishNet.Object;

public class GrappleHook : NetworkBehaviour
{
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;
    public List<Transform> hookPoints = new List<Transform>();

    private Vector2 _closestHook;
    private void Start()
    {
        foreach(GameObject Hp in GameObject.FindGameObjectsWithTag("Hook")) // hp == hoked Points
        { 
            hookPoints.Add(Hp.transform);
        }
        distanceJoint.enabled = false;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            
        }
        else
        {
            GetComponent<GrappleHook>().enabled = false;
        }
    }

    private void Update()
    {
        if (!base.IsOwner)
        {
            return;
        }
            GrappelHookToServer();
    }

    [ServerRpc]
    public void GrappelHookToServer()
    {
        
        GrappelHook();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    [ObserversRpc]
    private void GrappelHook()
    {
        if (base.IsOwner)
        {
            transform.position = transform.position;
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
                Debug.Log("down");

            }
            if(Input.GetKeyUp(KeyCode.Space))
            {
                distanceJoint.enabled = false;
                lineRenderer.enabled = false;
                Debug.Log("up");

            }

            if (distanceJoint.enabled)
            {
                lineRenderer.SetPosition(0, transform.position);
            }
        }
    }
}