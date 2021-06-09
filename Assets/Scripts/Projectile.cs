using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class Projectile : NetworkBehaviour
{
    public float Speed = 100;
    public float Lifespan = 3f;

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
        Debug.Log("OnDestroy called");
    }

}
