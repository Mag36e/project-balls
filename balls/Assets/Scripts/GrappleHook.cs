using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using FishNet;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Prediction;
using FishNet.Transporting.Tugboat;
using UnityEngine.Rendering;

public class GrappleHook : NetworkBehaviour
{
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;
    public List<Transform> hookPoints = new List<Transform>();

    public Vector2 _closestHook;
    public bool hooked;
    private void Start()
    {
        foreach(GameObject Hp in GameObject.FindGameObjectsWithTag("Hook")) // hp == hoked Points
        { 
            hookPoints.Add(Hp.transform);
        }
        Debug.Log("found "+ hookPoints.Count);
        distanceJoint.enabled = false;  
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner)
        {
            GetComponent<GrappleHook>().enabled = false;
        }
    }
    
    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        var transform1 = transform;
        transform1.position = transform1.position;
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                hooked = true;
                Debug.Log("TryingToHook");
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
                Hook();
            }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            hooked = false;
            HookLetgo();
        }
        
        if (distanceJoint.enabled)
        {
            lineRenderer.SetPosition(0, transform.position);
        }
    }
    

    private void FixedUpdate()
    {
        ServerConectionHook();
    }

    [ServerRpc]
    public void ServerConectionHook()
    {
        if (hooked == true)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, _closestHook);
            distanceJoint.connectedAnchor = _closestHook;
        }
        else
        {
            distanceJoint.enabled = false;
            lineRenderer.enabled = false;
        }
    }
    private void Hook()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, _closestHook);
        distanceJoint.connectedAnchor = _closestHook;
        distanceJoint.enabled = true;
        lineRenderer.enabled = true;
    }
    private void HookLetgo()
    {
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
    }

    
}