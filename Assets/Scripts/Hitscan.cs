using MLAPI.Messaging;
using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitscan : NetworkBehaviour
{
    public float fireRate = 0.5f;
    public float nextFire = 0.0f;
    public GameObject projectileSpawner;
    public TrailRenderer bulletTrail;

    private void Update()
    {
        if (IsLocalPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    ShootServerRpc();
                }
            }

        }
    }

    [ServerRpc]
    public void ShootServerRpc()
    {
        if (Physics.Raycast(projectileSpawner.transform.position, projectileSpawner.transform.forward, out RaycastHit hit, 2000f))
        {
            var enemyHealth = hit.transform.GetComponent<HealthComponent>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(200);
            }
        }

        ShootClientRpc();
    }
    [ClientRpc]
    public void ShootClientRpc()
    {
        // visual stuff
        var bullet = Instantiate(bulletTrail, projectileSpawner.transform.position, Quaternion.identity);
        bullet.AddPosition(projectileSpawner.transform.position);
        if (Physics.Raycast(projectileSpawner.transform.position, projectileSpawner.transform.forward, out RaycastHit hit, 2000f))
        {
            bullet.transform.position = hit.point;
        }
        else
        {
            bullet.transform.position = projectileSpawner.transform.position + (projectileSpawner.transform.forward * 2000);
        }
    }
}
