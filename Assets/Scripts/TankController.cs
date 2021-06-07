using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class TankController : NetworkBehaviour
{
    float maxLookDistance = 1000;
    float turretRotationSpeed = 100;
    public GameObject turret;
    public GameObject cannon;

    private void Start()
    {
        if (!IsLocalPlayer)
        {
            this.enabled = false;
        }
    }

    private void Update()
    {

        DoStuff();
    }

    public void DoStuff()
    {
        Vector3 aimPoint = GetAimpoint();
        Vector3 targetRotationTurret = aimPoint - turret.transform.position;
        Vector3 targetRotationCannon = aimPoint - cannon.transform.position;
        // Form a rotation facing the desired direction while keeping our
        // local axis vector exactly matching the current axis direction.
        Quaternion desiredRotationTurret = TurretLookRotation(targetRotationTurret, turret.transform.up);
        Quaternion desiredRotationCannon = CannonLookRotation(targetRotationCannon, turret.transform.right);
        // Move toward that rotation at a controlled, even speed regardless of framerate.
        turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, desiredRotationTurret, turretRotationSpeed * Time.deltaTime);
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
        if (Physics.Raycast(ray, out hit, maxLookDistance))
        {
            print("I'm looking at " + hit.transform.name + "at " + hit.transform.position);
            // raycast hit something, so return where it hit
            return hit.point;
        }
        print("I'm looking at nothing!");
        // raycast didn't hit anything, so return a position 1000 units in front of the camera
        return Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1000.0f));
    }
}
