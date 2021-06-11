using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class ProjectileSpawner : NetworkBehaviour
{
    public NetworkObject projectile;
    public float fireRate = 0.5f;
    public float nextFire = 0.0f;

    Rigidbody parentRb;
    private void Start()
    {
        parentRb = transform.root.GetComponent<Rigidbody>();
    }

    [ServerRpc]
    public void ShootServerRPC(Vector3 spawnPos, Quaternion rotation)
    {
        NetworkObject shotProjectile = Instantiate(projectile, spawnPos, rotation);

        // this is silly
        Collider cannonCollider = transform.parent.GetComponentInChildren<Collider>();

        Physics.IgnoreCollision(shotProjectile.GetComponent<Collider>(), cannonCollider);
        shotProjectile.GetComponent<Rigidbody>().velocity += parentRb.velocity;
        shotProjectile.Spawn();
    }
}
