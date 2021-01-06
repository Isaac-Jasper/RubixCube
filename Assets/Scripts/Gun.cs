using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public ParticleSystem muzzleBurst;
    public ParticleSystem onHit;
    public Camera mainCam;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
    }
    void shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit)) //make it s oit creates a new version of the on hit particle system and have it face the player
        {
            //onHit = new ParticleSystem();
            onHit.transform.position = hit.point;
            onHit.transform.localRotation = Quaternion.LookRotation(transform.position, transform.position);    
            onHit.Play();
        }
        muzzleBurst.Play();
    }
}
