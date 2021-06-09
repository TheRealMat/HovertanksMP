using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class Projectile : NetworkBehaviour
{
    public float Speed = 100;
    public float Lifespan = 3f;


    private void Start()
    {
        Destroy(gameObject, Lifespan);
    }

}
