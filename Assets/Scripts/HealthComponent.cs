using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;

public class HealthComponent : NetworkBehaviour
{
    [SerializeField]
    NetworkVariableInt health = new NetworkVariableInt(new NetworkVariableSettings { WritePermission = NetworkVariablePermission.OwnerOnly }, 100);

    PlayerRespawner playerSpawner;

    private void Start()
    {
        playerSpawner = GetComponent<PlayerRespawner>();
    }

    private void Update()
    {
        if (health.Value <= 0 && IsLocalPlayer)
        {
            playerSpawner.respawn();
            health.Value = 100;
        }
    }

    public void TakeDamage(int damage)
    {
        health.Value -= damage;
    }
}
