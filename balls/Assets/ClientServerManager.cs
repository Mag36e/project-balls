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
        InstanceFinder.ClientManager.StartConnection();
        InstanceFinder.ServerManager.StartConnection();
        /*if (isServer)
        {
            InstanceFinder.ServerManager.StartConnection();
        }
        else
        {
            InstanceFinder.ClientManager.StartConnection();
        }*/
    }
}
