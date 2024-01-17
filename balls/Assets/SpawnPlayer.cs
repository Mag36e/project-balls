using System.Collections;
using System.Collections.Generic;
using FishNet;
using FishNet.Connection;
using FishNet.Object;
using UnityEditor;
using UnityEngine;

public class SpawnPlayer : NetworkBehaviour
{
    [SerializeField] private GameObject Player;
    public override void OnStartClient()
    {
        base.OnStartClient();
        
        spawnplayer();
    }

    [ServerRpc(RequireOwnership = false)] //remnote precidule call
    public void spawnplayer(NetworkConnection client = null)
    {
        GameObject go = Instantiate(Player);
        
        Spawn(go,client);
    }
}
