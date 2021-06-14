using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class TankController : NetworkBehaviour
{
    float maxLookDistance = 1000;
    float turretRotationSpeed = 100;
    public GameObject turret;
    public GameObject cannon;
    public Hitscan projectileSpawner;

    Rigidbody rb;
    public float forwardAccel = 100f;
    public float backwardAccel = 25f;
    float currentThrust = 0f;
    public float turnStrength = 10f;
    float currentTurn = 0f;

    LayerMask layerMask;
    public float hoverForce = 9f;
    public float hoverHeight = 2f;
    public GameObject[] hoverPoints;


    private void Start()
    {
        if (!IsLocalPlayer)
        {
            this.enabled = false;
        }

        rb = GetComponent<Rigidbody>();

        layerMask = 1 << LayerMask.NameToLayer("Characters");
        layerMask = ~layerMask;
    }

    private void Update()
    {

        HandleRotation();

        // thrust
        currentThrust = 0f;
        float aclAxis = Input.GetAxis("Vertical");
        if (aclAxis > 0)
        {
            currentThrust = aclAxis * forwardAccel;
        }
        else if (aclAxis < 0)
        {
            currentThrust = aclAxis * backwardAccel;
        }

        // turning
        currentTurn = 0f;
        float turnAxis = Input.GetAxis("Horizontal");
        if (Mathf.Abs(turnAxis) > 0)
            currentTurn = turnAxis;
    }


    private void FixedUpdate()
    {

        // hover points
        RaycastHit hit;
        foreach (GameObject hoverPoint in hoverPoints)
        {
            // keep floating
            if (Physics.Raycast(hoverPoint.transform.position, -Vector3.up, out hit, hoverHeight, layerMask))
            {
                rb.AddForceAtPosition(Vector3.up * hoverForce * (1f - (hit.distance / hoverHeight)), hoverPoint.transform.position);

            }

            // keep upright
            else
            {
                Quaternion q = Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation;
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * hoverForce);
            }

        }




        // forwards
        if (Mathf.Abs(currentThrust) > 0)
        {
            // i think my gameobject is turned weird, oh well
            rb.AddForce(transform.right * -currentThrust);
        }

        // turn
        if (currentTurn > 0)
        {
            rb.AddRelativeTorque(Vector3.up * currentTurn * turnStrength);
        }
        else if (currentTurn < 0)
        {
            rb.AddRelativeTorque(Vector3.up * currentTurn * turnStrength);
        }




    }


    public void HandleRotation()
    {
        Vector3 aimPoint = GetAimpoint();
        Vector3 targetRotationTurret = aimPoint - turret.transform.position;
        Vector3 targetRotationCannon = aimPoint - cannon.transform.position;
        // Form a rotation facing the desired direction while keeping our
        // local axis vector exactly matching the current axis direction.
        Quaternion desiredRotationTurret = TurretLookRotation(targetRotationTurret, turret.transform.up);
        Quaternion desiredRotationCannon = CannonLookRotation(targetRotationCannon, turret.transform.right);
        // Move toward that rotation at a controlled, even speed regardless of framerate.
        // These should probably be clamped
        turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, desiredRotationTurret, turretRotationSpeed * Time.deltaTime);
        // Cannon will try to rotate 180 degrees if you spin camera fast
        cannon.transform.rotation = Quaternion.RotateTowards(cannon.transform.rotation, desiredRotationCannon, turretRotationSpeed * Time.deltaTime);
    }


    Quaternion TurretLookRotation(Vector3 approximateForward, Vector3 exactUp)
    {
        Quaternion rotateZToUp = Quaternion.LookRotation(exactUp, -approximateForward);
        Quaternion rotateYToZ = Quaternion.Euler(90f, 0f, 0f);

        return rotateZToUp * rotateYToZ;
    }

    Quaternion CannonLookRotation(Vector3 approximateForward, Vector3 exactRight)
    {
        Quaternion rotateXToRight = Quaternion.LookRotation(exactRight, -approximateForward);
        Quaternion rotateYToZ = Quaternion.Euler(0f, 0f, 90f);

        return rotateXToRight * rotateYToZ;
    }


    private Vector3 GetAimpoint()
    {
        // shoot ray from middle of screen
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxLookDistance, layerMask))
        {
            // raycast hit something, so return where it hit
            return hit.point;
        }
        // raycast didn't hit anything, so return a position 1000 units in front of the camera
        return Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1000.0f));
    }





}
