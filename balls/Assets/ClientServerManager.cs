using System;
using System.Collections;
using System.Collections.Generic;
using FishNet;
using UnityEngine;

public class ClientServerManager : MonoBehaviour
{
    [SerializeField] private bool isServer;
    private void Awake()
    {
        // for testing 
        
        /*InstanceFinder.ClientManager.StartConnection();
        InstanceFinder.ServerManager.StartConnection();*/
        
        // not for testing
        
        if (isServer)
        {
            InstanceFinder.ServerManager.StartConnection();
        }
        else
        {
            InstanceFinder.ClientManager.StartConnection();
        }
    }
}
