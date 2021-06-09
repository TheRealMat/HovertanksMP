using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class Projectile : NetworkBehaviour
{
    public float Speed = 100;
    public float Lifespan = 3f;

    public GameObject deathEffect;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(new Vector3(0, Speed, 0), ForceMode.Impulse);
        Destroy(gameObject, Lifespan);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
    void OnDestroy()
    {
        GameObject explosion = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);
        Debug.Log("OnDestroy called");
    }

}
