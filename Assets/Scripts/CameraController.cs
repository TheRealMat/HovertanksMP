using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class CameraController : NetworkBehaviour
{
    public float rotationPower;
    public float maxAngle = 340;
    public float minAngle = 40;
    float x;


    private void Start()
    {
        if (!IsLocalPlayer)
        {
            this.gameObject.SetActive(false);
        }
    }

    void Update()
    {

        float y = Input.GetAxis("Mouse X") * rotationPower;
        x += Input.GetAxis("Mouse Y") * rotationPower;

        x = Mathf.Clamp(x, minAngle, maxAngle);

        transform.eulerAngles = new Vector3(-x, transform.eulerAngles.y + y, 0);


    }
}
