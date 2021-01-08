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
        ParticleSystem shootClone;
        RaycastHit hit;

        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit) && hit.transform.CompareTag("Player") == false) //make it s oit creates a new version of the on hit particle system and have it face the player
        {
            shootClone = Instantiate(onHit, hit.point, onHit.transform.rotation);

            hit.collider.gameObject.GetComponent<Rigidbody>().velocity += velocity(hit.collider.gameObject, transform.position);
            //add it so if it hits an eney the particles switch to the enemy color/blood color
        }
        muzzleBurst.Play();
    }

    Vector3 velocity(GameObject hit, Vector3 pos)
    {
        Vector3 objectVelocity;
        int maxVelociy = 5;
        objectVelocity = hit.transform.position - pos;

        if (objectVelocity.x > maxVelociy) {//positive velocity checks
            objectVelocity.x = maxVelociy; //objectVelocity.x = maxVelociy - objectVelocity.x;
        } if (objectVelocity.y > maxVelociy) {
            objectVelocity.y = maxVelociy; //objectVelocity.y = maxVelociy - objectVelocity.y;
        } if (objectVelocity.z > maxVelociy) {
            objectVelocity.z = maxVelociy; //objectVelocity.z = maxVelociy - objectVelocity.z;
        } 
        
        if (objectVelocity.x < -maxVelociy) { //negative velocity checks
            objectVelocity.x = -maxVelociy; //objectVelocity.x = -maxVelociy + objectVelocity.x;
        } if (objectVelocity.y < -maxVelociy) {
            objectVelocity.y = -maxVelociy; //objectVelocity.y = -maxVelociy + objectVelocity.y;
        } if (objectVelocity.z < -maxVelociy) {
            objectVelocity.z = -maxVelociy; //objectVelocity.z = -maxVelociy + objectVelocity.z;
        } 
        // this code doesnt work and does weird stuff as expected
        return objectVelocity;
    }
}
