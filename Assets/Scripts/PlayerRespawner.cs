using MLAPI.Messaging;
using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawner : NetworkBehaviour
{
    [ServerRpc]
    void RespawnServerRPC()
    {
        RespawnClientRPC(GetRandomSpawn());
    }


    public void respawn()
    {
        RespawnServerRPC();
    }
    [ClientRpc]
    void RespawnClientRPC(Vector3 spawnPos)
    {
        transform.position = spawnPos;
    }
    Vector3 GetRandomSpawn()
    {
        float x = Random.Range(-50f, 50f);
        float y = 2f;
        float z = Random.Range(-50f, 50f);
        return new Vector3(x, y, z);
    }
}
