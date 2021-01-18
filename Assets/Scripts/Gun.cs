using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public PlayerStats playerStats;
    public ParticleSystem muzzleBurst;
    public ParticleSystem onHit;
    public ParticleSystem onHitEnemy;
    public Camera mainCam;
    public int layerMask = 1 << 2;
    public float shotDistance = 100;

    private void Start()
    {
        layerMask = ~layerMask;
    }

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
        muzzleBurst.Play();

        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, shotDistance, ~0, QueryTriggerInteraction.Ignore))
        {
            
            if (hit.collider.CompareTag("Enemy"))
            {
                shootClone = Instantiate(onHitEnemy, hit.point, onHit.transform.rotation);
                hit.collider.GetComponent<FlyingEnemy1>().Shot(playerStats.damage);
            }
            else
            {
                shootClone = Instantiate(onHit, hit.point, onHit.transform.rotation);
            }

            if(hit.collider.gameObject.TryGetComponent(out Rigidbody RB))
                RB.velocity += velocity(hit.collider.gameObject, transform.position);
        }
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
